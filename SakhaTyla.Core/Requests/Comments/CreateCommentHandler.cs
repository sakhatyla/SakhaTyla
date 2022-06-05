using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Security;

namespace SakhaTyla.Core.Requests.Comments
{
    public class CreateCommentHandler : IRequestHandler<CreateComment, CreatedEntity<int>>
    {
        private readonly IEntityRepository<Comment> _commentRepository;
        private readonly IEntityRepository<CommentContainer> _commentContainerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserInfoProvider _userInfoProvider;
        private readonly IMapper _mapper;

        public CreateCommentHandler(IEntityRepository<Comment> commentRepository,
            IEntityRepository<CommentContainer> commentContainerRepository,
            IUnitOfWork unitOfWork,
            IUserInfoProvider userInfoProvider,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _commentContainerRepository = commentContainerRepository;
            _unitOfWork = unitOfWork;
            _userInfoProvider = userInfoProvider;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreateComment request, CancellationToken cancellationToken)
        {
            var user = await _userInfoProvider.GetUserInfoAsync();
            var comment = _mapper.Map<CreateComment, Comment>(request);
            var commentContainer = await GetCommentContainerAsync(comment.ContainerId);
            if (user.User != null)
            {
                comment.AuthorId = user.UserId;
            }
            _commentRepository.Add(comment);
            commentContainer.CommentCount += 1;
            await _unitOfWork.CommitAsync(cancellationToken);
            await _commentRepository.CalculateTree(comment);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new CreatedEntity<int>(comment.Id);
        }

        private async Task<CommentContainer> GetCommentContainerAsync(int commentContainerId)
        {
            var commentContainer = await _commentContainerRepository.GetEntities()
                .Include(e => e.Page!.Route)
                .Where(e => e.Id == commentContainerId)
                .FirstOrDefaultAsync();
            if (commentContainer == null)
            {
                throw new ServiceException($"Comment container {commentContainerId} not found");
            }
            return commentContainer;
        }
    }
}
