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

        public ArticleMigration(SourceLoader sourceLoader,
            IMediator mediator,
            DataContext dataContext,
            IEntityRepository<Tag> tagRepository,
            IEntityRepository<Category> categoryRepository,
            IEntityRepository<Language> languageRepository)
        {
            _sourceLoader = sourceLoader;
            _mediator = mediator;
            _dataContext = dataContext;
            _tagRepository = tagRepository;
            _categoryRepository = categoryRepository;
            _languageRepository = languageRepository;
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
                .ToDictionary(g => g.Key);

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
                await CreateArticleAsync(createOrUpdateArticle);
            }
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
    }
}
