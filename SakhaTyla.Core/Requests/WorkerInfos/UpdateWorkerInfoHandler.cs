using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.WorkerInfos
{
    public class UpdateWorkerInfoHandler : IRequestHandler<UpdateWorkerInfo>
    {
        private readonly IEntityRepository<WorkerInfo> _workerInfoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateWorkerInfoHandler(IEntityRepository<WorkerInfo> workerInfoRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _workerInfoRepository = workerInfoRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateWorkerInfo request, CancellationToken cancellationToken)
        {
            var workerInfo = await _workerInfoRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (workerInfo == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Worker"], request.Id]);
            }
            _mapper.Map(request, workerInfo);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
