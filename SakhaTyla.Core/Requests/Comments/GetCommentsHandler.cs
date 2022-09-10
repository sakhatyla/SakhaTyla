using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Comments.Models;

namespace SakhaTyla.Core.Requests.Comments
{
    public class GetCommentsHandler : IRequestHandler<GetComments, PageModel<CommentModel>>
    {
        private readonly IEntityRepository<Comment> _commentRepository;
        private readonly IMapper _mapper;

        public GetCommentsHandler(IEntityRepository<Comment> commentRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<CommentModel>> Handle(GetComments request, CancellationToken cancellationToken)
        {
            IQueryable<Comment> query = _commentRepository.GetEntities()
                .Include(e => e.Container.Page)
                .Include(e => e.Author)
                .Include(e => e.Parent);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var comments = await query.ToPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);
            var model = comments.Map<Comment, CommentModel>(_mapper);
            if (request.SkipChildren == true)
            {
                foreach (var commentModel in model.PageItems)
                {
                    commentModel.Children = null!;
                }
            }
            return model;
        }

    }
}
