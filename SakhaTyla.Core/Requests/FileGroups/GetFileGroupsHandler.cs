using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.FileGroups.Models;

namespace SakhaTyla.Core.Requests.FileGroups
{
    public class GetFileGroupsHandler : IRequestHandler<GetFileGroups, PageModel<FileGroupModel>>
    {
        private readonly IEntityRepository<FileGroup> _fileGroupRepository;
        private readonly IMapper _mapper;

        public GetFileGroupsHandler(IEntityRepository<FileGroup> fileGroupRepository,
            IMapper mapper)
        {
            _fileGroupRepository = fileGroupRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<FileGroupModel>> Handle(GetFileGroups request, CancellationToken cancellationToken)
        {
            IQueryable<FileGroup> query = _fileGroupRepository.GetEntities();            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var fileGroups = await query.ToPagedListAsync(request.PageIndex, request.PageSize);
            return fileGroups.Map<FileGroup, FileGroupModel>(_mapper);
        }

    }
}
