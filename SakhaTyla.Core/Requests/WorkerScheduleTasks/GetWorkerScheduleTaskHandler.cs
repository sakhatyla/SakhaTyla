using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.WorkerScheduleTasks.Models;

namespace SakhaTyla.Core.Requests.WorkerScheduleTasks
{
    public class GetWorkerScheduleTaskHandler : IRequestHandler<GetWorkerScheduleTask, WorkerScheduleTaskModel?>
    {
        private readonly IEntityRepository<WorkerScheduleTask> _workerScheduleTaskRepository;
        private readonly IMapper _mapper;

        public GetWorkerScheduleTaskHandler(IEntityRepository<WorkerScheduleTask> workerScheduleTaskRepository,
            IMapper mapper)
        {
            _workerScheduleTaskRepository = workerScheduleTaskRepository;
            _mapper = mapper;
        }

        public async Task<WorkerScheduleTaskModel?> Handle(GetWorkerScheduleTask request, CancellationToken cancellationToken)
        {
            var workerScheduleTask = await _workerScheduleTaskRepository.GetEntities()
                .Include(e => e.WorkerInfo)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (workerScheduleTask == null)
            {
                return null;
            }
            return _mapper.Map<WorkerScheduleTask, WorkerScheduleTaskModel>(workerScheduleTask);
        }

    }
}
