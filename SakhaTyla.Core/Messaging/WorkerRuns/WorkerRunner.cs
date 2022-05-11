using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Messaging;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Messaging.WorkerRuns
{
    public class WorkerRunner : IWorkerRunner
    {
        private readonly IEntityRepository<WorkerRun> _workerRunRepository;
        private readonly IEntityRepository<WorkerInfo> _workerInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagingService _messagingService;

        public WorkerRunner(IEntityRepository<WorkerRun> workerRunRepository,
            IEntityRepository<WorkerInfo> workerInfoRepository,
            IUnitOfWork unitOfWork,
            IMessagingService messagingService)
        {
            _workerRunRepository = workerRunRepository;
            _workerInfoRepository = workerInfoRepository;
            _unitOfWork = unitOfWork;
            _messagingService = messagingService;
        }

        public async Task<int> RunAsync(Type workerType, object? data)
        {
            var workerInfo = await _workerInfoRepository.GetEntities()
                .FirstOrDefaultAsync(e => e.ClassName == workerType.FullName);
            if (workerInfo == null)
            {
                throw new ServiceException($"{workerType.Name} not found");
            }
            var workerRun = new WorkerRun
            {
                WorkerInfoId = workerInfo.Id,
                Data = data != null ? JsonSerializer.Serialize(data) : null,
            };
            _workerRunRepository.Add(workerRun);
            await _unitOfWork.CommitAsync();
            await _messagingService.SendAsync(StartWorkerRun.QueueName, new StartWorkerRun(workerRun.Id));

            return workerRun.Id;
        }
    }
}
