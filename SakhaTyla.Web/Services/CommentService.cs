using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Comments;
using SakhaTyla.Core.Requests.Comments.Models;
using SakhaTyla.Web.Protos;
using SakhaTyla.Web.Protos.Comments;

namespace SakhaTyla.Web.Services
{
    [Authorize("ReadComment")]
    public class CommentService : Protos.Comments.CommentService.CommentServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CommentService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<CommentPageModel> GetComments(GetCommentsRequest getCommentsRequest, ServerCallContext context)
        {
            var getComments = _mapper.Map<GetCommentsRequest, GetComments>(getCommentsRequest);
            var model = await _mediator.Send(getComments);
            return _mapper.Map<PageModel<CommentModel>, CommentPageModel>(model);
        }

        public override async Task<Comment> GetComment(GetCommentRequest getCommentRequest, ServerCallContext context)
        {
            var getComment = _mapper.Map<GetCommentRequest, GetComment>(getCommentRequest);
            var model = await _mediator.Send(getComment);
            return _mapper.Map<CommentModel, Comment>(model!);
        }

        [Authorize("WriteComment")]
        public override async Task<Empty> UpdateComment(UpdateCommentRequest updateCommentRequest, ServerCallContext context)
        {
            var updateComment = _mapper.Map<UpdateCommentRequest, UpdateComment>(updateCommentRequest);
            var model = await _mediator.Send(updateComment);
            return _mapper.Map<Unit, Empty>(model);
        }

        [Authorize("WriteComment")]
        public override async Task<CreatedEntity> CreateComment(CreateCommentRequest createCommentRequest, ServerCallContext context)
        {
            var createComment = _mapper.Map<CreateCommentRequest, CreateComment>(createCommentRequest);
            var model = await _mediator.Send(createComment);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(model);
        }

        [Authorize("WriteComment")]
        public override async Task<Empty> DeleteComment(DeleteCommentRequest deleteCommentRequest, ServerCallContext context)
        {
            var deleteComment = _mapper.Map<DeleteCommentRequest, DeleteComment>(deleteCommentRequest);
            var model = await _mediator.Send(deleteComment);
            return _mapper.Map<Unit, Empty>(model);
        }
    }
}
