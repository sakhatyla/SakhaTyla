using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Pages;
using SakhaTyla.Core.Requests.Pages.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadPage")]
    [ValidateModel]
    [Route("api")]
    public class PageController : Controller
    {
        private readonly IMediator _mediator;

        public PageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetPages")]
        public async Task<PageModel<PageModel>> GetPagesAsync([FromBody] GetPages getPages)
        {
            return await _mediator.Send(getPages);
        }

        [HttpPost("GetPage")]
        public async Task<PageModel?> GetPageAsync([FromBody] GetPage getPage)
        {
            return await _mediator.Send(getPage);
        }

        [HttpPost("ExportPages")]
        public async Task<FileResult> ExportPagesAsync([FromBody] ExportPages exportPages)
        {
            var file = await _mediator.Send(exportPages);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WritePage")]
        [HttpPost("UpdatePage")]
        public async Task<Unit> UpdatePageAsync([FromBody] UpdatePage updatePage)
        {
            return await _mediator.Send(updatePage);
        }

        [Authorize("WritePage")]
        [HttpPost("CreatePage")]
        public async Task<CreatedEntity<int>> CreatePageAsync([FromBody] CreatePage createPage)
        {
            return await _mediator.Send(createPage);
        }

        [Authorize("WritePage")]
        [HttpPost("DeletePage")]
        public async Task<Unit> DeletePageAsync([FromBody] DeletePage deletePage)
        {
            return await _mediator.Send(deletePage);
        }
    }
}