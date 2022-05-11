using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.WorkerScheduleTasks.Models;

namespace SakhaTyla.Core.Requests.WorkerScheduleTasks
{
    public class GetWorkerScheduleTasksHandler : IRequestHandler<GetWorkerScheduleTasks, PageModel<WorkerScheduleTaskModel>>
    {
        private readonly IEntityRepository<WorkerScheduleTask> _workerScheduleTaskRepository;
        private readonly IMapper _mapper;

        public GetWorkerScheduleTasksHandler(IEntityRepository<WorkerScheduleTask> workerScheduleTaskRepository,
            IMapper mapper)
        {
            _workerScheduleTaskRepository = workerScheduleTaskRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<WorkerScheduleTaskModel>> Handle(GetWorkerScheduleTasks request, CancellationToken cancellationToken)
        {
            IQueryable<WorkerScheduleTask> query = _workerScheduleTaskRepository.GetEntities()
                .Include(e => e.WorkerInfo);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var workerScheduleTasks = await query.ToPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);
            return workerScheduleTasks.Map<WorkerScheduleTask, WorkerScheduleTaskModel>(_mapper);
        }

    }
}
