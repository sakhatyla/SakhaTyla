using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.FileStorage;

namespace SakhaTyla.Core.Requests.Files
{
    public class DeleteFileHandler : IRequestHandler<DeleteFile>
    {
        private readonly IEntityRepository<File> _fileRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorage _fileStorage;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteFileHandler(IEntityRepository<File> fileRepository,
            IUnitOfWork unitOfWork,
            IFileStorage fileStorage,
            IStringLocalizer<SharedResource> localizer)
        {
            _fileRepository = fileRepository;
            _unitOfWork = unitOfWork;
            _fileStorage = fileStorage;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeleteFile request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetEntities()
                .Include(e => e.Group)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (file == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["File"], request.Id]);
            }
            if (file.Group.Type == Enums.FileGroupType.Storage)
            {
                if (file.Url == null)
                {
                    throw new Exception($"File {file.Id} Url is empty");
                }
                await _fileStorage.DeleteFileAsync(file.Url);
            }
            _fileRepository.Delete(file);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
