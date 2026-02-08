using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Requests.Public.BookLabels;
using SakhaTyla.Core.Requests.Public.BookLabels.Models;

namespace SakhaTyla.Web.Controllers.Public
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [ValidateModel]
    [Route("api/public")]
    public class BookLabelController : Controller
    {
        private readonly IMediator _mediator;

        public BookLabelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetBookLabels")]
        public async Task<PageModel<BookLabelModel>> GetBookLabelsAsync([FromBody] GetBookLabels getBookLabels)
        {
            return await _mediator.Send(getBookLabels);
        }
    }
}
