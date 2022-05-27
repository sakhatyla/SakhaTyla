using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Tags;
using SakhaTyla.Core.Requests.Tags.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadTag")]
    [ValidateModel]
    [Route("api")]
    public class TagController : Controller
    {
        private readonly IMediator _mediator;

        public TagController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetTags")]
        public async Task<PageModel<TagModel>> GetTagsAsync([FromBody] GetTags getTags)
        {
            return await _mediator.Send(getTags);
        }

        [HttpPost("GetTag")]
        public async Task<TagModel?> GetTagAsync([FromBody] GetTag getTag)
        {
            return await _mediator.Send(getTag);
        }

        [HttpPost("ExportTags")]
        public async Task<FileResult> ExportTagsAsync([FromBody] ExportTags exportTags)
        {
            var file = await _mediator.Send(exportTags);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteTag")]
        [HttpPost("UpdateTag")]
        public async Task<Unit> UpdateTagAsync([FromBody] UpdateTag updateTag)
        {
            return await _mediator.Send(updateTag);
        }

        [Authorize("WriteTag")]
        [HttpPost("CreateTag")]
        public async Task<CreatedEntity<int>> CreateTagAsync([FromBody] CreateTag createTag)
        {
            return await _mediator.Send(createTag);
        }

        [Authorize("WriteTag")]
        [HttpPost("DeleteTag")]
        public async Task<Unit> DeleteTagAsync([FromBody] DeleteTag deleteTag)
        {
            return await _mediator.Send(deleteTag);
        }
    }
}