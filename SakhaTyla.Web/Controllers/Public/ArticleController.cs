using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Requests.Public.Articles;
using SakhaTyla.Core.Requests.Public.Articles.Models;

namespace SakhaTyla.Web.Controllers.Public
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [ValidateModel]
    [Route("api/public")]
    public class ArticleController : Controller
    {
        private readonly IMediator _mediator;

        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("SearchArticlesByTitle")]
        public async Task<List<ArticleModel>> SearchArticlesByTitleAsync([FromBody] SearchArticlesByTitle searchArticlesByTitle)
        {
            return await _mediator.Send(searchArticlesByTitle);
        }
    }
}
