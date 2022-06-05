using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.Comments
{
    public class DeleteCommentHandler : IRequestHandler<DeleteComment>
    {
        private readonly IEntityRepository<Comment> _commentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteCommentHandler(IEntityRepository<Comment> commentRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> localizer)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeleteComment request, CancellationToken cancellationToken)
        {
            var comment = await _commentRepository.GetEntities()
                .Include(e => e.Container)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (comment == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Comment"], request.Id]);
            }
            _commentRepository.Delete(comment);
            comment.Container.CommentCount--;
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
