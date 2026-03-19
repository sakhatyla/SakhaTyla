using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.FileStorage;
using SakhaTyla.Core.Formatters;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Articles;
using SakhaTyla.Core.SpellCheck;

namespace SakhaTyla.Core.Workers
{
    public class SpellingFixWorker : IWorker
    {
        private readonly IEntityRepository<Article> _articleRepository;
        private readonly IEntityRepository<Language> _languageRepository;
        private readonly ISpellCheckService _spellCheckService;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IFileStorage _fileStorage;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDataContext _dataContext;
        private readonly ILogger<SpellingFixWorker> _logger;

        private const int BatchSize = 1000;
        private const string SahLanguageCode = "sah";

        public SpellingFixWorker(IEntityRepository<Article> articleRepository,
            IEntityRepository<Language> languageRepository,
            ISpellCheckService spellCheckService,
            IExcelFormatter excelFormatter,
            IFileStorage fileStorage,
            IUnitOfWork unitOfWork,
            IDataContext dataContext,
            ILogger<SpellingFixWorker> logger)
        {
            _articleRepository = articleRepository;
            _languageRepository = languageRepository;
            _spellCheckService = spellCheckService;
            _excelFormatter = excelFormatter;
            _fileStorage = fileStorage;
            _unitOfWork = unitOfWork;
            _dataContext = dataContext;
            _logger = logger;
        }

        public async Task ExecuteAsync(WorkerContext workerContext)
        {
            // Не работает правильно, исправляет правильные слова            
            throw new Exception("Disabled");

            var data = !string.IsNullOrEmpty(workerContext.Data)
                ? JsonSerializer.Deserialize<SpellingFixData>(workerContext.Data)
                : null;
            var saveMode = data?.SaveMode ?? false;

            var sahLanguage = await _languageRepository.GetEntities()
                .Where(l => l.Code == SahLanguageCode)
                .FirstOrDefaultAsync(workerContext.CancellationToken)
                ?? throw new Exception($"Language with code '{SahLanguageCode}' not found");

            // Articles where exactly one of (FromLanguage, ToLanguage) is "sah"
            var baseQuery = _articleRepository.GetEntities()
                .Include(e => e.FromLanguage)
                .Include(e => e.ToLanguage)
                .Where(e => !e.IsDeleted)
                .Where(e => (e.FromLanguageId == sahLanguage.Id || e.ToLanguageId == sahLanguage.Id)
                         && !(e.FromLanguageId == sahLanguage.Id && e.ToLanguageId == sahLanguage.Id))
                .OrderBy(e => e.Id);

            var fixes = new List<SpellingFixModel>();
            var totalArticleCount = 0;
            var lastId = 0;

            while (true)
            {
                var batch = await baseQuery
                    .Where(e => e.Id > lastId)
                    .Take(BatchSize)
                    .ToListAsync(workerContext.CancellationToken);

                if (batch.Count == 0)
                    break;

                foreach (var article in batch)
                {
                    var otherLanguageCode = article.FromLanguageId == sahLanguage.Id
                        ? article.ToLanguage.Code
                        : article.FromLanguage.Code;
                    var fixedText = _spellCheckService.FixSpelling(SahLanguageCode, article.TextSource,
                        new[] { otherLanguageCode });

                    if (fixedText != article.TextSource)
                    {
                        fixes.Add(new SpellingFixModel
                        {
                            Original = article.TextSource,
                            Fixed = fixedText,
                        });

                        if (saveMode)
                        {
                            article.TextSource = fixedText;
                            article.Text = fixedText.ProcessArticleText();
                        }
                    }
                }

                if (saveMode)
                {
                    await _unitOfWork.CommitAsync(workerContext.CancellationToken);
                }

                totalArticleCount += batch.Count;
                lastId = batch[^1].Id;

                _logger.LogInformation("Processed {ArticleCount} articles, found {FixCount} fixes so far",
                    totalArticleCount, fixes.Count);

                _dataContext.ClearChangeTracker();
            }

            // Save results to Excel
            using var stream = new MemoryStream();
            await _excelFormatter.SaveToAsync(stream, fixes, true);
            stream.Position = 0;

            var filePath = $"spelling-fixes/spelling-fixes-{DateTime.UtcNow:yyyyMMdd-HHmmss}.xlsx";
            var url = await _fileStorage.SaveFileAsync(filePath, stream,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");

            workerContext.ResultData = url;
            workerContext.Result = saveMode
                ? $"Found {fixes.Count} fixes in {totalArticleCount} articles. Saved {fixes.Count} fixes. Excel: {url}"
                : $"Found {fixes.Count} fixes in {totalArticleCount} articles. Excel: {url}";

            _logger.LogInformation(workerContext.Result);
        }
    }

    public class SpellingFixData
    {
        public bool SaveMode { get; set; }
    }

    public class SpellingFixModel
    {
        public string? Original { get; set; }
        public string? Fixed { get; set; }
    }
}
