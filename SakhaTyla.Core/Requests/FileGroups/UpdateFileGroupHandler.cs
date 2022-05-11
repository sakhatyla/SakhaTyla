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

namespace SakhaTyla.Core.Requests.FileGroups
{
    public class UpdateFileGroupHandler : IRequestHandler<UpdateFileGroup>
    {
        private readonly IEntityRepository<FileGroup> _fileGroupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateFileGroupHandler(IEntityRepository<FileGroup> fileGroupRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _fileGroupRepository = fileGroupRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateFileGroup request, CancellationToken cancellationToken)
        {
            var fileGroup = await _fileGroupRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (fileGroup == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["File Group"], request.Id]);
            }
            _mapper.Map(request, fileGroup);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
