using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Tags.Models;

namespace SakhaTyla.Core.Requests.Tags
{
    public class GetTagsHandler : IRequestHandler<GetTags, PageModel<TagModel>>
    {
        private readonly IEntityRepository<Tag> _tagRepository;
        private readonly IMapper _mapper;

        public GetTagsHandler(IEntityRepository<Tag> tagRepository,
            IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<TagModel>> Handle(GetTags request, CancellationToken cancellationToken)
        {
            IQueryable<Tag> query = _tagRepository.GetEntities();            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var tags = await query.ToPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);
            return tags.Map<Tag, TagModel>(_mapper);
        }

    }
}
