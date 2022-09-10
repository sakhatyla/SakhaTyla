using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SakhaTyla.Migration.SourceDatabase;
using SakhaTyla.Core.Requests.Categories;
using SakhaTyla.Data;
using SakhaTyla.Migration.Data;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using System.Text.Json;

namespace SakhaTyla.Migration.Migrations
{
    class ArticleMigration
    {
        private readonly SourceLoader _sourceLoader;
        private readonly IMediator _mediator;
        private readonly DataContext _dataContext;
        private readonly IEntityRepository<Tag> _tagRepository;
        private readonly IEntityRepository<Category> _categoryRepository;
        private readonly IEntityRepository<Language> _languageRepository;
        private readonly IEntityRepository<User> _userRepository;

        public ArticleMigration(SourceLoader sourceLoader,
            IMediator mediator,
            DataContext dataContext,
            IEntityRepository<Tag> tagRepository,
            IEntityRepository<Category> categoryRepository,
            IEntityRepository<Language> languageRepository,
            IEntityRepository<User> userRepository)
        {
            _sourceLoader = sourceLoader;
            _mediator = mediator;
            _dataContext = dataContext;
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
            _languageRepository = languageRepository;
            _userRepository = userRepository;
        }

        public async Task MigrateArticles()
        {
            var tags = await _tagRepository.GetEntities()
                .ToListAsync();
            var tagMap = tags.ToDictionary(t => t.Name, t => t.Id);

            var categories = await _categoryRepository.GetEntities()
                .ToListAsync();
            var categoryMap = categories.ToDictionary(c => c.Name, c => c.Id);

            var languages = await _languageRepository.GetEntities()
                .ToListAsync();
            var languageMap = languages.ToDictionary(c => c.Name, c => c.Id);

            var allArticleTags = await _sourceLoader.GetArticleTagsAsync();
            var articleTagsByArticleId = allArticleTags.GroupBy(e => e.ArticleId)
                .ToDictionary(g => g.Key, g => g.ToList());

            var allArticleHistories = await _sourceLoader.GetArticleHistoriesAsync();
            var articleHistoriesByArticleId = allArticleHistories.GroupBy(e => e.ArticleId)
                .ToDictionary(g => g.Key, g => g.ToList());

            var users = await _userRepository.GetEntities()
                .ToListAsync();
            var userMap = users.ToDictionary(u => u.Email, u => u.Id);

            var articles = await _sourceLoader.GetArticlesAsync();
            foreach (var article in articles)
            {
                var createOrUpdateArticle = new CreateOrUpdateArticle()
                {
                    Title = article.Title,
                    TextSource = article.TextSource,
                    Text = article.Text,
                    FromLanguageId = languageMap[article.FromLanguageName],
                    ToLanguageId = languageMap[article.ToLanguageName],
                    Fuzzy = article.Fuzzy,
                    CategoryId = !string.IsNullOrEmpty(article.CategoryName) ? categoryMap[article.CategoryName] : null,
                    IsDeleted = article.IsDeleted,
                    CreationDate = article.DateCreated.UtcDateTime,
                    ModificationDate = article.DateModified.UtcDateTime,
                };
                if (articleTagsByArticleId.TryGetValue(article.Id, out var articleTags))
                {
                    createOrUpdateArticle.TagIds = articleTags.Select(e => tagMap[e.TagName]).ToArray();
                }
                if (articleHistoriesByArticleId.TryGetValue(article.Id, out var articleHistories))
                {
                    createOrUpdateArticle.Changes = new List<CreateEntityChange>();
                    SrcArticleHistory? prevArticleHistory = null;
                    foreach (var articleHistory in articleHistories)
                    {
                        var change = new CreateEntityChange()
                        {
                            EntityName = "Article",
                            Action = MapAction(articleHistory.Type),
                            CreationUserId = userMap[articleHistory.UserCreatedEmail],
                            CreationDate = articleHistory.DateCreated.UtcDateTime,
                            From = "",
                            To = "",
                        };
                        if (change.Action != Core.Enums.ChangeAction.Add)
                        {
                            if (prevArticleHistory == null)
                            {
                                throw new Exception("Previous history not found");
                            }
                            var fromArticle = new Article(prevArticleHistory.NewTitle!, prevArticleHistory.NewTextSource!)
                            {
                                Fuzzy = prevArticleHistory.NewFuzzy,
                            };
                            change.From = JsonSerializer.Serialize(fromArticle);
                        }
                        if (change.Action != Core.Enums.ChangeAction.Delete)
                        {
                            var toArticle = new Article(articleHistory.NewTitle!, articleHistory.NewTextSource!)
                            {
                                Fuzzy = articleHistory.NewFuzzy,
                            };
                            change.To = JsonSerializer.Serialize(toArticle);
                        }
                        createOrUpdateArticle.Changes.Add(change);
                        prevArticleHistory = articleHistory;
                    }
                }
                await CreateArticleAsync(createOrUpdateArticle);
            }
        }

        private Core.Enums.ChangeAction MapAction(HistoryType type)
        {
            switch(type)
            {
                case HistoryType.Created:
                    return Core.Enums.ChangeAction.Add;
                case HistoryType.Updated:
                    return Core.Enums.ChangeAction.Update;
                case HistoryType.Deleted:
                    return Core.Enums.ChangeAction.Delete;
            }
            throw new Exception("Wrong type");
        }

        private async Task CreateArticleAsync(CreateOrUpdateArticle article)
        {
            var idEntity = _dataContext.Set<IdEntity>().FromSqlInterpolated($@"
insert into Articles
(
Title,
Text,
TextSource,
FromLanguageId,
ToLanguageId,
Fuzzy,
CategoryId,
IsDeleted,
CreationDate,
ModificationDate
)
values
(
{article.Title},
{article.Text},
{article.TextSource},
{article.FromLanguageId},
{article.ToLanguageId},
{article.Fuzzy},
{article.CategoryId},
{article.IsDeleted},
{article.CreationDate},
{article.ModificationDate}
)

declare @articleId int = @@IDENTITY
select @articleId as Id
").AsEnumerable().First();

            if (article.TagIds != null)
            {
                foreach (var articleTagId in article.TagIds)
                {
                    await _dataContext.Database.ExecuteSqlInterpolatedAsync($@"
insert into ArticleTags
(
ArticleId,
TagId,
CreationDate,
ModificationDate
)
values
(
{idEntity.Id},
{articleTagId},
GETUTCDATE(), 
GETUTCDATE()
)");
                }
            }

            if (article.Changes != null)
            {
                foreach (var change in article.Changes)
                {
                    await _dataContext.Database.ExecuteSqlInterpolatedAsync($@"
insert into EntityChanges
(
EntityName,
EntityId,
Action,
[From],
[To],
CreationDate,
CreationUserId
)
values
(
{change.EntityName},
{idEntity.Id},
{change.Action}, 
{change.From},
{change.To},
{change.CreationDate},
{change.CreationUserId}
)");
                }
            }
        }
    }

    public class CreateOrUpdateArticle
    {
        public string? Title { get; set; }

        public string? Text { get; set; }

        public string? TextSource { get; set; }

        public int? FromLanguageId { get; set; }

        public int? ToLanguageId { get; set; }

        public bool? Fuzzy { get; set; }

        public int? CategoryId { get; set; }

        public int[]? TagIds { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public List<CreateEntityChange>? Changes { get; set; }
    }

    public class CreateEntityChange
    {
        public string EntityName { get; set; } = null!;

        public Core.Enums.ChangeAction Action { get; set; }

        public string? From { get; set; }

        public string? To { get; set; }

        public DateTime CreationDate { get; set; }

        public int? CreationUserId { get; set; }
    }
}
