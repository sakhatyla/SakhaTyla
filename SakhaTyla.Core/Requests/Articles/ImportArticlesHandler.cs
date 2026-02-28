using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Messaging;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.FileStorage;
using SakhaTyla.Core.Formatters;
using SakhaTyla.Core.Indexers;
using SakhaTyla.Core.Messaging.Articles;
using SakhaTyla.Core.Requests.Articles.Models;

namespace SakhaTyla.Core.Requests.Articles
{
    public class ImportArticlesHandler : IRequestHandler<ImportArticles, Unit>
    {
        private readonly IEntityRepository<Article> _articleRepository;
        private readonly IEntityRepository<Category> _categoryRepository;
        private readonly IEntityRepository<Entities.File> _fileRepository;
        private readonly IFileStorage _fileStorage;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagingService _messagingService;

        public ImportArticlesHandler(IEntityRepository<Article> articleRepository,
            IEntityRepository<Category> categoryRepository,
            IEntityRepository<Entities.File> fileRepository,
            IFileStorage fileStorage,
            IExcelFormatter excelFormatter,
            IUnitOfWork unitOfWork,
            IMessagingService messagingService)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _fileRepository = fileRepository;
            _fileStorage = fileStorage;
            _excelFormatter = excelFormatter;
            _unitOfWork = unitOfWork;
            _messagingService = messagingService;
        }

        public async Task<Unit> Handle(ImportArticles request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetEntities()
                .Include(e => e.Group)
                .Where(e => e.Id == request.FileId)
                .FirstAsync(cancellationToken);

            byte[] content;
            if (file.Group.Type == Enums.FileGroupType.Database)
            {
                content = file.Content ?? throw new Exception($"File {file.Id} Content is empty");
            }
            else if (file.Group.Type == Enums.FileGroupType.Storage)
            {
                content = await _fileStorage.DownloadFileAsync(file.Url ?? throw new Exception($"File {file.Id} Url is empty"));
            }
            else
            {
                throw new NotSupportedException($"Group type {file.Group.Type} not supported");
            }

            using var stream = new MemoryStream(content);
            var importModels = await _excelFormatter.LoadFromAsync<ArticleImportModel>(stream, true);

            var categories = await _categoryRepository.GetEntities().ToListAsync(cancellationToken);

            foreach (var model in importModels)
            {
                if (string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.TextSource))
                {
                    continue;
                }

                int? categoryId = null;
                if (!string.IsNullOrEmpty(model.Category))
                {
                    var category = categories.FirstOrDefault(c =>
                        string.Equals(c.Name, model.Category, StringComparison.OrdinalIgnoreCase));
                    if (category == null)
                    {
                        category = new Category(model.Category);
                        _categoryRepository.Add(category);
                        await _unitOfWork.CommitAsync(cancellationToken);
                        categories.Add(category);
                    }
                    categoryId = category.Id;
                }

                var article = new Article(model.Title, model.TextSource)
                {
                    Text = model.TextSource.ProcessArticleText(),
                    FromLanguageId = request.FromLanguageId ?? 0,
                    ToLanguageId = request.ToLanguageId ?? 0,
                    CategoryId = categoryId,
                };
                _articleRepository.Add(article);
                await _unitOfWork.CommitAsync(cancellationToken);

                await _messagingService.SendAsync(UpdateArticleIndex.QueueName,
                    new UpdateArticleIndex() { Id = article.Id, Action = IndexAction.Add });
            }

            return Unit.Value;
        }
    }
}
