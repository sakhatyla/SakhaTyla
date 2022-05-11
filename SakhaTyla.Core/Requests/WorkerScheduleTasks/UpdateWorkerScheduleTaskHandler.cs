using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Messaging;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Messaging.WorkerInfos;

namespace SakhaTyla.Core.Requests.WorkerScheduleTasks
{
    public class UpdateWorkerScheduleTaskHandler : IRequestHandler<UpdateWorkerScheduleTask>
    {
        private readonly IEntityRepository<WorkerScheduleTask> _workerScheduleTaskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessagingService _messagingService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateWorkerScheduleTaskHandler(IEntityRepository<WorkerScheduleTask> workerScheduleTaskRepository,
            IUnitOfWork unitOfWork,
            IMessagingService messagingService,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _workerScheduleTaskRepository = workerScheduleTaskRepository;
            _unitOfWork = unitOfWork;
            _messagingService = messagingService;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateWorkerScheduleTask request, CancellationToken cancellationToken)
        {
            var workerScheduleTask = await _workerScheduleTaskRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (workerScheduleTask == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Worker Schedule Task"], request.Id]);
            }
            _mapper.Map(request, workerScheduleTask);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _messagingService.SendAsync(ScheduleWorkerInfo.QueueName, new ScheduleWorkerInfo
            {
                Id = workerScheduleTask.WorkerInfoId
            });
            return Unit.Value;
        }

    }
}
