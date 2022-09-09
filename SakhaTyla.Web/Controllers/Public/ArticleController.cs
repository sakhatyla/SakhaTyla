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

        [HttpPost("Translate")]
        public async Task<TranslateModel> TranslateAsync([FromBody] Translate translate)
        {
            return await _mediator.Send(translate);
        }

        [HttpPost("SuggestArticles")]
        public async Task<List<ArticleSuggestModel>> SuggestArticlesAsync([FromBody] SuggestArticles suggestArticles)
        {
            return await _mediator.Send(suggestArticles);
        }

        [HttpPost("GetRandomArticle")]
        public async Task<ArticleModel> GetRandomArticleAsync([FromBody] GetRandomArticle getRandomArticle)
        {
            return await _mediator.Send(getRandomArticle);
        }
    }
}
