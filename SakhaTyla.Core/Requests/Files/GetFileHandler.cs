using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Files.Models;

namespace SakhaTyla.Core.Requests.Files
{
    public class GetFileHandler : IRequestHandler<GetFile, FileModel?>
    {
        private readonly IEntityRepository<File> _fileRepository;
        private readonly IMapper _mapper;

        public GetFileHandler(IEntityRepository<File> fileRepository,
            IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<FileModel?> Handle(GetFile request, CancellationToken cancellationToken)
        {
            var file = await _fileRepository.GetEntities()
                .Include(e => e.Group)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (file == null)
            {
                return null;
            }
            return _mapper.Map<File, FileModel>(file);
        }

    }
}
