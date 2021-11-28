using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.WorkerRuns.Models;

namespace SakhaTyla.Core.Requests.WorkerRuns
{
    public class GetWorkerRunsHandler : IRequestHandler<GetWorkerRuns, PageModel<WorkerRunModel>>
    {
        private readonly IEntityRepository<WorkerRun> _workerRunRepository;
        private readonly IMapper _mapper;

        public GetWorkerRunsHandler(IEntityRepository<WorkerRun> workerRunRepository,
            IMapper mapper)
        {
            _workerRunRepository = workerRunRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<WorkerRunModel>> Handle(GetWorkerRuns request, CancellationToken cancellationToken)
        {
            IQueryable<WorkerRun> query = _workerRunRepository.GetEntities()
                .Include(e => e.WorkerInfo);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var workerRuns = await query.ToPagedListAsync(request.PageIndex, request.PageSize);
            return workerRuns.Map<WorkerRun, WorkerRunModel>(_mapper);
        }

    }
}
