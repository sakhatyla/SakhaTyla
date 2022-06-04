using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.CommentContainers.Models;

namespace SakhaTyla.Core.Requests.CommentContainers
{
    public class GetCommentContainerHandler : IRequestHandler<GetCommentContainer, CommentContainerModel?>
    {
        private readonly IEntityRepository<CommentContainer> _commentContainerRepository;
        private readonly IMapper _mapper;

        public GetCommentContainerHandler(IEntityRepository<CommentContainer> commentContainerRepository,
            IMapper mapper)
        {
            _commentContainerRepository = commentContainerRepository;
            _mapper = mapper;
        }

        public async Task<CommentContainerModel?> Handle(GetCommentContainer request, CancellationToken cancellationToken)
        {
            var commentContainer = await _commentContainerRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (commentContainer == null)
            {
                return null;
            }
            return _mapper.Map<CommentContainer, CommentContainerModel>(commentContainer);
        }

    }
}
