using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SakhaTyla.Core.Messaging.WorkerRuns;
using SakhaTyla.Core.Workers;

namespace SakhaTyla.Core.Requests.Articles
{
    public class ImportArticlesHandler : IRequestHandler<ImportArticles, Unit>
    {
        private readonly IWorkerRunner _workerRunner;

        public ImportArticlesHandler(IWorkerRunner workerRunner)
        {
            _workerRunner = workerRunner;
        }

        public async Task<Unit> Handle(ImportArticles request, CancellationToken cancellationToken)
        {
            await _workerRunner.RunAsync(typeof(ArticleImportWorker), new ArticleImportData
            {
                FromLanguageId = request.FromLanguageId ?? 0,
                ToLanguageId = request.ToLanguageId ?? 0,
                FileId = request.FileId ?? 0,
            });
            return Unit.Value;
        }
    }
}
