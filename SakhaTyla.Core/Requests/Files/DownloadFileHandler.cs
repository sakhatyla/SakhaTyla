using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.FileStorage;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Files.Models;

namespace SakhaTyla.Core.Requests.Files
{
    public class DownloadFileHandler : IRequestHandler<DownloadFile, FileContentModel>
    {
        private readonly IEntityRepository<File> _fileRepository;
        private readonly IFileStorage _fileStorage;

        public DownloadFileHandler(IEntityRepository<File> fileRepository,
            IFileStorage fileStorage)
        {
            _fileRepository = fileRepository;
            _fileStorage = fileStorage;
        }

        public async Task<FileContentModel> Handle(DownloadFile request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetEntities()
                .Include(e => e.Group)
                .Where(e => e.Id == request.Id)
                .FirstAsync(cancellationToken);
            byte[] content;
            if (file.Group.Type == Enums.FileGroupType.Database)
            {
                if (file.Content == null)
                {
                    throw new Exception($"File {file.Id} Content is empty");
                }
                content = file.Content;
            }
            else if (file.Group.Type == Enums.FileGroupType.Storage)
            {
                if (file.Url == null)
                {
                    throw new Exception($"File {file.Id} Url is empty");
                }
                content = await _fileStorage.DownloadFileAsync(file.Url);
            }
            else
            {
                throw new NotSupportedException($"Group type {file.Group.Type} not supported");
            }
            return new FileContentModel(file.Name, content, file.ContentType);
        }

    }
}
