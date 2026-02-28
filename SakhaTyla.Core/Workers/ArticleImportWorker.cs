using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Cynosura.Core.Data;
using Cynosura.Core.Messaging;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.FileStorage;
using SakhaTyla.Core.Formatters;
using SakhaTyla.Core.Indexers;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Messaging.Articles;
using SakhaTyla.Core.Requests.Articles;
using SakhaTyla.Core.Requests.Articles.Models;

namespace SakhaTyla.Core.Workers
{
    public class ArticleImportWorker : IWorker
    {
        private readonly IEntityRepository<Article> _articleRepository;
        private readonly IEntityRepository<Category> _categoryRepository;
        private readonly IEntityRepository<Entities.File> _fileRepository;
        private readonly IFileStorage _fileStorage;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataContext _dataContext;
        private readonly IMessagingService _messagingService;
        private readonly ILogger<ArticleImportWorker> _logger;

        private const int BatchSize = 1000;

        public ArticleImportWorker(IEntityRepository<Article> articleRepository,
            IEntityRepository<Category> categoryRepository,
            IEntityRepository<Entities.File> fileRepository,
            IFileStorage fileStorage,
            IExcelFormatter excelFormatter,
            IUnitOfWork unitOfWork,
            IDataContext dataContext,
            IMessagingService messagingService,
            ILogger<ArticleImportWorker> logger)
        {
            _articleRepository = articleRepository;
            _categoryRepository = categoryRepository;
            _fileRepository = fileRepository;
            _fileStorage = fileStorage;
            _excelFormatter = excelFormatter;
            _unitOfWork = unitOfWork;
            _dataContext = dataContext;
            _messagingService = messagingService;
            _logger = logger;
        }

        public async Task ExecuteAsync(WorkerContext workerContext)
        {
            var data = JsonSerializer.Deserialize<ArticleImportData>(workerContext.Data!);

            var file = await _fileRepository.GetEntities()
                .Include(e => e.Group)
                .Where(e => e.Id == data!.FileId)
                .FirstAsync(workerContext.CancellationToken);

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

            var categories = await _categoryRepository.GetEntities().ToListAsync(workerContext.CancellationToken);

            var count = 0;
            var batchCount = 0;
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
                        await _unitOfWork.CommitAsync(workerContext.CancellationToken);
                        categories.Add(category);
                    }
                    categoryId = category.Id;
                }

                var article = new Article(model.Title, model.TextSource)
                {
                    Text = model.TextSource.ProcessArticleText(),
                    FromLanguageId = data!.FromLanguageId,
                    ToLanguageId = data.ToLanguageId,
                    CategoryId = categoryId,
                };
                _articleRepository.Add(article);
                await _unitOfWork.CommitAsync(workerContext.CancellationToken);

                await _messagingService.SendAsync(UpdateArticleIndex.QueueName,
                    new UpdateArticleIndex() { Id = article.Id, Action = IndexAction.Add });
                count++;
                batchCount++;

                if (batchCount >= BatchSize)
                {
                    _dataContext.ClearChangeTracker();
                    batchCount = 0;
                }
            }

            workerContext.Result = $"Imported {count} articles";
        }
    }

    public class ArticleImportData
    {
        public int FromLanguageId { get; set; }
        public int ToLanguageId { get; set; }
        public int FileId { get; set; }
    }
}
