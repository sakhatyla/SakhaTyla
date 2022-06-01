using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Books;
using SakhaTyla.Core.Requests.Books.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadBook")]
    [ValidateModel]
    [Route("api")]
    public class BookController : Controller
    {
        private readonly IMediator _mediator;

        public BookController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetBooks")]
        public async Task<PageModel<BookModel>> GetBooksAsync([FromBody] GetBooks getBooks)
        {
            return await _mediator.Send(getBooks);
        }

        [HttpPost("GetBook")]
        public async Task<BookModel?> GetBookAsync([FromBody] GetBook getBook)
        {
            return await _mediator.Send(getBook);
        }

        [HttpPost("ExportBooks")]
        public async Task<FileResult> ExportBooksAsync([FromBody] ExportBooks exportBooks)
        {
            var file = await _mediator.Send(exportBooks);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteBook")]
        [HttpPost("UpdateBook")]
        public async Task<Unit> UpdateBookAsync([FromBody] UpdateBook updateBook)
        {
            return await _mediator.Send(updateBook);
        }

        [Authorize("WriteBook")]
        [HttpPost("CreateBook")]
        public async Task<CreatedEntity<int>> CreateBookAsync([FromBody] CreateBook createBook)
        {
            return await _mediator.Send(createBook);
        }

        [Authorize("WriteBook")]
        [HttpPost("DeleteBook")]
        public async Task<Unit> DeleteBookAsync([FromBody] DeleteBook deleteBook)
        {
            return await _mediator.Send(deleteBook);
        }
    }
}