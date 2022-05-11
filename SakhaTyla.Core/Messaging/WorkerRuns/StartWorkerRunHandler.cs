using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Workers;

namespace SakhaTyla.Core.Messaging.WorkerRuns
{
    public class StartWorkerRunHandler : IRequestHandler<StartWorkerRun>
    {
        private readonly IEntityRepository<WorkerRun> _workerRunRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<StartWorkerRunHandler> _logger;
        private readonly IServiceProvider _serviceProvider;

        public StartWorkerRunHandler(IEntityRepository<WorkerRun> workerRunRepository,
            IUnitOfWork unitOfWork,
            ILogger<StartWorkerRunHandler> logger,
            IServiceProvider serviceProvider)
        {
            _workerRunRepository = workerRunRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task<Unit> Handle(StartWorkerRun request, CancellationToken cancellationToken)
        {
            var workerRun = await _workerRunRepository.GetEntities()
                .Include(e => e.WorkerInfo)
                .Where(e => e.Id == request.WorkerRunId)
                .FirstOrDefaultAsync(cancellationToken);
            if (workerRun != null)
            {
                if (workerRun.Status != Enums.WorkerRunStatus.New)
                {
                    _logger.LogError($"Can't start WorkerRun {request.WorkerRunId} (status is {workerRun.Status})");
                    return Unit.Value;
                }

                await SetWorkerRunStartAsync(workerRun.Id);

                var assemblies = CoreHelper.GetPlatformAndAppAssemblies();
                var type = assemblies.SelectMany(a => a.GetTypes()).Where(t => t.FullName == workerRun.WorkerInfo.ClassName).First();

                var workerContext = new WorkerContext(workerRun.Data, cancellationToken);

                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var worker = (IWorker?)scope.ServiceProvider.GetService(type);
                        if (worker == null)
                        {
                            throw new Exception($"Worker with type {type} not found");
                        }
                        await worker.ExecuteAsync(workerContext);
                    }
                    await SetWorkerRunEndAsync(workerRun.Id, Enums.WorkerRunStatus.Completed, workerContext.Result, workerContext.ResultData);
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception, "Worker run failed");
                    await SetWorkerRunEndAsync(workerRun.Id, Enums.WorkerRunStatus.Error, exception.ToString(), null);
                }
            }
            else
            {
                _logger.LogError($"WorkerRun {request.WorkerRunId} not found");
            }
            return Unit.Value;
        }

        private async Task SetWorkerRunStartAsync(int workerRunId)
        {
            var workerRun = await _workerRunRepository.GetEntities()
                .Where(e => e.Id == workerRunId)
                .FirstAsync();
            workerRun.StartDateTime = DateTime.UtcNow;
            workerRun.Status = Enums.WorkerRunStatus.Running;
            await _unitOfWork.CommitAsync();
        }

        private static string? PrepareResult(string? result)
        {
            if (result == null)
            {
                return null;
            }
            // Remove 0x00 chars
            result = result.Replace("\0", "");
            return result;
        }

        private async Task SetWorkerRunEndAsync(int workerRunId, Enums.WorkerRunStatus status, string? result, string? resultData)
        {
            var workerRun = await _workerRunRepository.GetEntities()
                .Where(e => e.Id == workerRunId)
                .FirstAsync();
            workerRun.EndDateTime = DateTime.UtcNow;
            workerRun.Status = status;
            workerRun.Result = PrepareResult(result);
            workerRun.ResultData = resultData;
            await _unitOfWork.CommitAsync();
        }
    }
}
