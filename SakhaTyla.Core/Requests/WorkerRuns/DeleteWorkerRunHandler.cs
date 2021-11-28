using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.WorkerRuns
{
    public class DeleteWorkerRunHandler : IRequestHandler<DeleteWorkerRun>
    {
        private readonly IEntityRepository<WorkerRun> _workerRunRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteWorkerRunHandler(IEntityRepository<WorkerRun> workerRunRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> localizer)
        {
            _workerRunRepository = workerRunRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeleteWorkerRun request, CancellationToken cancellationToken)
        {
            var workerRun = await _workerRunRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync();
            if (workerRun == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Worker Run"], request.Id]);
            }
            _workerRunRepository.Delete(workerRun);
            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }

    }
}
