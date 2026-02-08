using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Requests.Public.Books;
using SakhaTyla.Core.Requests.Public.Books.Models;

namespace SakhaTyla.Web.Controllers.Public
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [ValidateModel]
    [Route("api/public")]
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
    }
}
