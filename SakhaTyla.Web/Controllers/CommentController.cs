using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Comments;
using SakhaTyla.Core.Requests.Comments.Models;
using SakhaTyla.Core.Requests.CommentContainers;
using SakhaTyla.Core.Requests.CommentContainers.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadComment")]
    [ValidateModel]
    [Route("api")]
    public class CommentController : Controller
    {
        private readonly IMediator _mediator;

        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetComments")]
        public async Task<PageModel<CommentModel>> GetCommentsAsync([FromBody] GetComments getComments)
        {
            return await _mediator.Send(getComments);
        }

        [HttpPost("GetComment")]
        public async Task<CommentModel?> GetCommentAsync([FromBody] GetComment getComment)
        {
            return await _mediator.Send(getComment);
        }

        [HttpPost("GetCommentContainer")]
        public async Task<CommentContainerModel?> GetCommentContainerAsync([FromBody] GetCommentContainer getCommentContainer)
        {
            return await _mediator.Send(getCommentContainer);
        }

        [HttpPost("ExportComments")]
        public async Task<FileResult> ExportCommentsAsync([FromBody] ExportComments exportComments)
        {
            var file = await _mediator.Send(exportComments);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteComment")]
        [HttpPost("UpdateComment")]
        public async Task<Unit> UpdateCommentAsync([FromBody] UpdateComment updateComment)
        {
            return await _mediator.Send(updateComment);
        }

        [Authorize("WriteComment")]
        [HttpPost("CreateComment")]
        public async Task<CreatedEntity<int>> CreateCommentAsync([FromBody] CreateComment createComment)
        {
            return await _mediator.Send(createComment);
        }

        [Authorize("WriteComment")]
        [HttpPost("DeleteComment")]
        public async Task<Unit> DeleteCommentAsync([FromBody] DeleteComment deleteComment)
        {
            return await _mediator.Send(deleteComment);
        }
    }
}
