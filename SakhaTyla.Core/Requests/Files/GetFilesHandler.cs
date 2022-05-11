using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Files.Models;

namespace SakhaTyla.Core.Requests.Files
{
    public class GetFilesHandler : IRequestHandler<GetFiles, PageModel<FileModel>>
    {
        private readonly IEntityRepository<File> _fileRepository;
        private readonly IMapper _mapper;

        public GetFilesHandler(IEntityRepository<File> fileRepository,
            IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<FileModel>> Handle(GetFiles request, CancellationToken cancellationToken)
        {
            IQueryable<File> query = _fileRepository.GetEntities()
                .Include(e => e.Group);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var files = await query.ToPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);
            return files.Map<File, FileModel>(_mapper);
        }

    }
}
