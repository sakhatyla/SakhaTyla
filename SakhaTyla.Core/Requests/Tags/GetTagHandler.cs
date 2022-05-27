using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Tags.Models;

namespace SakhaTyla.Core.Requests.Tags
{
    public class GetTagHandler : IRequestHandler<GetTag, TagModel?>
    {
        private readonly IEntityRepository<Tag> _tagRepository;
        private readonly IMapper _mapper;

        public GetTagHandler(IEntityRepository<Tag> tagRepository,
            IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<TagModel?> Handle(GetTag request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (tag == null)
            {
                return null;
            }
            return _mapper.Map<Tag, TagModel>(tag);
        }

    }
}
