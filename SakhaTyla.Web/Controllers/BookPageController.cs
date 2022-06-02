using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.BookPages;
using SakhaTyla.Core.Requests.BookPages.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadBookPage")]
    [ValidateModel]
    [Route("api")]
    public class BookPageController : Controller
    {
        private readonly IMediator _mediator;

        public BookPageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetBookPages")]
        public async Task<PageModel<BookPageModel>> GetBookPagesAsync([FromBody] GetBookPages getBookPages)
        {
            return await _mediator.Send(getBookPages);
        }

        [HttpPost("GetBookPage")]
        public async Task<BookPageModel?> GetBookPageAsync([FromBody] GetBookPage getBookPage)
        {
            return await _mediator.Send(getBookPage);
        }

        [HttpPost("ExportBookPages")]
        public async Task<FileResult> ExportBookPagesAsync([FromBody] ExportBookPages exportBookPages)
        {
            var file = await _mediator.Send(exportBookPages);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteBookPage")]
        [HttpPost("UpdateBookPage")]
        public async Task<Unit> UpdateBookPageAsync([FromBody] UpdateBookPage updateBookPage)
        {
            return await _mediator.Send(updateBookPage);
        }

        [Authorize("WriteBookPage")]
        [HttpPost("CreateBookPage")]
        public async Task<CreatedEntity<int>> CreateBookPageAsync([FromBody] CreateBookPage createBookPage)
        {
            return await _mediator.Send(createBookPage);
        }

        [Authorize("WriteBookPage")]
        [HttpPost("DeleteBookPage")]
        public async Task<Unit> DeleteBookPageAsync([FromBody] DeleteBookPage deleteBookPage)
        {
            return await _mediator.Send(deleteBookPage);
        }
    }
}