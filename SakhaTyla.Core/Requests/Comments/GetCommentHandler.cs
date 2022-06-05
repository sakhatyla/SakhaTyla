using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Comments.Models;

namespace SakhaTyla.Core.Requests.Comments
{
    public class GetCommentHandler : IRequestHandler<GetComment, CommentModel?>
    {
        private readonly IEntityRepository<Comment> _commentRepository;
        private readonly IMapper _mapper;

        public GetCommentHandler(IEntityRepository<Comment> commentRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<CommentModel?> Handle(GetComment request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetEntities()
                .Include(e => e.Container)
                .Include(e => e.Author)
                .Include(e => e.Parent)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (comment == null)
            {
                return null;
            }
            return _mapper.Map<Comment, CommentModel>(comment);
        }

    }
}
