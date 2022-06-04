using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.BookAuthors;
using SakhaTyla.Core.Requests.BookAuthors.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadBookAuthor")]
    [ValidateModel]
    [Route("api")]
    public class BookAuthorController : Controller
    {
        private readonly IMediator _mediator;

        public BookAuthorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetBookAuthors")]
        public async Task<PageModel<BookAuthorModel>> GetBookAuthorsAsync([FromBody] GetBookAuthors getBookAuthors)
        {
            return await _mediator.Send(getBookAuthors);
        }

        [HttpPost("GetBookAuthor")]
        public async Task<BookAuthorModel?> GetBookAuthorAsync([FromBody] GetBookAuthor getBookAuthor)
        {
            return await _mediator.Send(getBookAuthor);
        }

        [HttpPost("ExportBookAuthors")]
        public async Task<FileResult> ExportBookAuthorsAsync([FromBody] ExportBookAuthors exportBookAuthors)
        {
            var file = await _mediator.Send(exportBookAuthors);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteBookAuthor")]
        [HttpPost("UpdateBookAuthor")]
        public async Task<Unit> UpdateBookAuthorAsync([FromBody] UpdateBookAuthor updateBookAuthor)
        {
            return await _mediator.Send(updateBookAuthor);
        }

        [Authorize("WriteBookAuthor")]
        [HttpPost("CreateBookAuthor")]
        public async Task<CreatedEntity<int>> CreateBookAuthorAsync([FromBody] CreateBookAuthor createBookAuthor)
        {
            return await _mediator.Send(createBookAuthor);
        }

        [Authorize("WriteBookAuthor")]
        [HttpPost("DeleteBookAuthor")]
        public async Task<Unit> DeleteBookAuthorAsync([FromBody] DeleteBookAuthor deleteBookAuthor)
        {
            return await _mediator.Send(deleteBookAuthor);
        }
    }
}