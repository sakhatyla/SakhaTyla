using System;
using System.IO;
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
using SakhaTyla.Core.FileStorage;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Files
{
    public class CreateFileHandler : IRequestHandler<CreateFile, CreatedEntity<int>>
    {
        private readonly IEntityRepository<Entities.File> _fileRepository;
        private readonly IEntityRepository<FileGroup> _fileGroupRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public CreateFileHandler(IEntityRepository<Entities.File> fileRepository,
            IEntityRepository<FileGroup> fileGroupRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IFileStorage fileStorage,
            IStringLocalizer<SharedResource> localizer)
        {
            _fileRepository = fileRepository;
            _fileGroupRepository = fileGroupRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStorage = fileStorage;
            _localizer = localizer;
        }

        public async Task<CreatedEntity<int>> Handle(CreateFile request, CancellationToken cancellationToken)
        {
            var fileGroup = await _fileGroupRepository.GetEntities()
                .Where(e => e.Id == request.GroupId)
                .FirstOrDefaultAsync(cancellationToken);
            if (fileGroup == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["File Group"], request.GroupId!]);
            }
            fileGroup.Accept.Validate(request.Name!, request.ContentType!);
            var file = _mapper.Map<CreateFile, Entities.File>(request);
            if (fileGroup.Type == Enums.FileGroupType.Database)
            {
                file.Content = request.Content!.ConvertToBytes();
            }
            else if (fileGroup.Type == Enums.FileGroupType.Storage)
            {
                var filename = Guid.NewGuid() + Path.GetExtension(request.Name);
                var filePath = $"{fileGroup.Location}/{DateTime.Today:yyyy'/'MM}/{filename}";
                file.Url = await _fileStorage.SaveFileAsync(filePath, request.Content!, request.ContentType!);
            }
            else
            {
                throw new NotSupportedException($"Group type {fileGroup.Type} not supported");
            }
            _fileRepository.Add(file);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new CreatedEntity<int>(file.Id);
        }
    }
}
