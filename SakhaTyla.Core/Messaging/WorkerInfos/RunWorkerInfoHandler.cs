using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Cynosura.Core.Data;
using Cynosura.Core.Messaging;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Messaging.WorkerRuns;
using MediatR;

namespace SakhaTyla.Core.Messaging.WorkerInfos
{
    public class RunWorkerInfoHandler : IRequestHandler<RunWorkerInfo>
    {
        private readonly IEntityRepository<WorkerRun> _workerRunRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagingService _messagingService;

        public RunWorkerInfoHandler(IEntityRepository<WorkerRun> workerRunRepository,
            IUnitOfWork unitOfWork,
            IMessagingService messagingService)
        {
            _workerRunRepository = workerRunRepository;
            _unitOfWork = unitOfWork;
            _messagingService = messagingService;
        }

        public async Task<Unit> Handle(RunWorkerInfo request, CancellationToken cancellationToken)
        {
            var workerRun = new WorkerRun
            {
                WorkerInfoId = request.Id,
                Data = request.Data != null ? JsonSerializer.Serialize(request.Data) : null,
            };
            _workerRunRepository.Add(workerRun);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _messagingService.SendAsync(StartWorkerRun.QueueName, new StartWorkerRun(workerRun.Id));
            return Unit.Value;
        }
    }
}
