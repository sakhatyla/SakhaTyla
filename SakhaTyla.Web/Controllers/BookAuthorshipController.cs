using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.BookAuthorships;
using SakhaTyla.Core.Requests.BookAuthorships.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadBookAuthorship")]
    [ValidateModel]
    [Route("api")]
    public class BookAuthorshipController : Controller
    {
        private readonly IMediator _mediator;

        public BookAuthorshipController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetBookAuthorships")]
        public async Task<PageModel<BookAuthorshipModel>> GetBookAuthorshipsAsync([FromBody] GetBookAuthorships getBookAuthorships)
        {
            return await _mediator.Send(getBookAuthorships);
        }

        [HttpPost("GetBookAuthorship")]
        public async Task<BookAuthorshipModel?> GetBookAuthorshipAsync([FromBody] GetBookAuthorship getBookAuthorship)
        {
            return await _mediator.Send(getBookAuthorship);
        }

        [HttpPost("ExportBookAuthorships")]
        public async Task<FileResult> ExportBookAuthorshipsAsync([FromBody] ExportBookAuthorships exportBookAuthorships)
        {
            var file = await _mediator.Send(exportBookAuthorships);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteBookAuthorship")]
        [HttpPost("UpdateBookAuthorship")]
        public async Task<Unit> UpdateBookAuthorshipAsync([FromBody] UpdateBookAuthorship updateBookAuthorship)
        {
            return await _mediator.Send(updateBookAuthorship);
        }

        [Authorize("WriteBookAuthorship")]
        [HttpPost("CreateBookAuthorship")]
        public async Task<CreatedEntity<int>> CreateBookAuthorshipAsync([FromBody] CreateBookAuthorship createBookAuthorship)
        {
            return await _mediator.Send(createBookAuthorship);
        }

        [Authorize("WriteBookAuthorship")]
        [HttpPost("DeleteBookAuthorship")]
        public async Task<Unit> DeleteBookAuthorshipAsync([FromBody] DeleteBookAuthorship deleteBookAuthorship)
        {
            return await _mediator.Send(deleteBookAuthorship);
        }
    }
}