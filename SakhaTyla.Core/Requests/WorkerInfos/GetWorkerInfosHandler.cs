using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.WorkerInfos.Models;

namespace SakhaTyla.Core.Requests.WorkerInfos
{
    public class GetWorkerInfosHandler : IRequestHandler<GetWorkerInfos, PageModel<WorkerInfoModel>>
    {
        private readonly IEntityRepository<WorkerInfo> _workerInfoRepository;
        private readonly IMapper _mapper;

        public GetWorkerInfosHandler(IEntityRepository<WorkerInfo> workerInfoRepository,
            IMapper mapper)
        {
            _workerInfoRepository = workerInfoRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<WorkerInfoModel>> Handle(GetWorkerInfos request, CancellationToken cancellationToken)
        {
            IQueryable<WorkerInfo> query = _workerInfoRepository.GetEntities();            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var workerInfos = await query.ToPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);
            return workerInfos.Map<WorkerInfo, WorkerInfoModel>(_mapper);
        }

    }
}
