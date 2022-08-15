using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Public.BookPages;
using SakhaTyla.Core.Requests.Public.BookPages.Models;

namespace SakhaTyla.Web.Controllers.Public
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [ValidateModel]
    [Route("api/public")]
    public class BookPageController : Controller
    {
        private readonly IMediator _mediator;

        public BookPageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetBookPageByNumber")]
        public async Task<BookPageModel?> GetBookPageByNumberAsync([FromBody] GetBookPageByNumber getBookPageByNumber)
        {
            return await _mediator.Send(getBookPageByNumber);
        }
    }
}
