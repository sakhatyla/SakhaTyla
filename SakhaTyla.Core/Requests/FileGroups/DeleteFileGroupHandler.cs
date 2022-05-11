using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.FileGroups
{
    public class DeleteFileGroupHandler : IRequestHandler<DeleteFileGroup>
    {
        private readonly IEntityRepository<FileGroup> _fileGroupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteFileGroupHandler(IEntityRepository<FileGroup> fileGroupRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> localizer)
        {
            _fileGroupRepository = fileGroupRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeleteFileGroup request, CancellationToken cancellationToken)
        {
            var fileGroup = await _fileGroupRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (fileGroup == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["File Group"], request.Id]);
            }
            _fileGroupRepository.Delete(fileGroup);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
