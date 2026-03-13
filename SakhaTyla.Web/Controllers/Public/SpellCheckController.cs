using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Requests.Public.SpellCheck;
using SakhaTyla.Core.Requests.Public.SpellCheck.Models;

namespace SakhaTyla.Web.Controllers.Public
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [ValidateModel]
    [Route("api/public")]
    public class SpellCheckController : Controller
    {
        private readonly IMediator _mediator;

        public SpellCheckController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("FixSpelling")]
        public async Task<FixSpellingModel> FixSpellingAsync([FromBody] FixSpelling fixSpelling)
        {
            return await _mediator.Send(fixSpelling);
        }
    }
}
