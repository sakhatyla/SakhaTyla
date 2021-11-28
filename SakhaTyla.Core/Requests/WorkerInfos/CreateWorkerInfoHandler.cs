using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.WorkerInfos
{
    public class CreateWorkerInfoHandler : IRequestHandler<CreateWorkerInfo, CreatedEntity<int>>
    {
        private readonly IEntityRepository<WorkerInfo> _workerInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateWorkerInfoHandler(IEntityRepository<WorkerInfo> workerInfoRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _workerInfoRepository = workerInfoRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreateWorkerInfo request, CancellationToken cancellationToken)
        {
            var workerInfo = _mapper.Map<CreateWorkerInfo, WorkerInfo>(request);
            _workerInfoRepository.Add(workerInfo);
            await _unitOfWork.CommitAsync();
            return new CreatedEntity<int>() { Id = workerInfo.Id };
        }

    }
}
