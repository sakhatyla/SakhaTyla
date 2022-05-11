using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.FileGroups.Models;

namespace SakhaTyla.Core.Requests.FileGroups
{
    public class GetFileGroupHandler : IRequestHandler<GetFileGroup, FileGroupModel?>
    {
        private readonly IEntityRepository<FileGroup> _fileGroupRepository;
        private readonly IMapper _mapper;

        public GetFileGroupHandler(IEntityRepository<FileGroup> fileGroupRepository,
            IMapper mapper)
        {
            _fileGroupRepository = fileGroupRepository;
            _mapper = mapper;
        }

        public async Task<FileGroupModel?> Handle(GetFileGroup request, CancellationToken cancellationToken)
        {
            var query = _fileGroupRepository.GetEntities();
            if (request.Id != null)
                query = query.Where(e => e.Id == request.Id);
            else
                query = query.Where(e => e.Name == request.Name);
            var fileGroup = await query.FirstOrDefaultAsync(cancellationToken);
            if (fileGroup == null)
            {
                return null;
            }
            return _mapper.Map<FileGroup, FileGroupModel>(fileGroup);
        }

    }
}
