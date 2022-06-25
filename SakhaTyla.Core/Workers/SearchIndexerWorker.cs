using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SakhaTyla.Core.Indexers;
using SakhaTyla.Core.Search;

namespace SakhaTyla.Core.Workers
{
    public class SearchIndexerWorker : IWorker
    {
        private readonly ISearchIndexWriter _searchIndexWriter;
        private readonly ArticleIndexer _articleIndexer;
        private readonly ILogger<SearchIndexerWorker> _logger;

        public SearchIndexerWorker(ISearchIndexWriter searchIndexWriter,
            ArticleIndexer articleIndexer,
            ILogger<SearchIndexerWorker> logger)
        {
            _searchIndexWriter = searchIndexWriter;
            _articleIndexer = articleIndexer;
            _logger = logger;
        }

        public async Task ExecuteAsync(WorkerContext workerContext)
        {
            var data = !string.IsNullOrEmpty(workerContext.Data) ?
                JsonSerializer.Deserialize<SearchIndexerData>(workerContext.Data) :
                null;
            var empty = _searchIndexWriter.GetDocumentCount() == 0;
            var result = "";

            if (empty)
            {
                _logger.LogInformation("Index is empty, creating...");

                await _articleIndexer.IndexArticlesAsync();

                _searchIndexWriter.Commit();

                result += "Index successfully created; ";
                _logger.LogInformation($"Index successfully created");
            }
            else
            {
                result += "Index already exists; ";
                if (data?.Recreate == true)
                {
                    _logger.LogInformation("Recreating index...");
                    result += "Recreating index...; ";

                    _searchIndexWriter.DeleteAll();

                    await _articleIndexer.IndexArticlesAsync();

                    _searchIndexWriter.Commit();

                    result += "Index successfully created; ";
                    _logger.LogInformation($"Index successfully created");
                }
            }
            workerContext.Result = result;
        }
        
    }

    public class SearchIndexerData
    {
        public bool Recreate { get; set; }
    }
}
