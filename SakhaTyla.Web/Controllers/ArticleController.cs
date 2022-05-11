using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Articles;
using SakhaTyla.Core.Requests.Articles.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadArticle")]
    [ValidateModel]
    [Route("api")]
    public class ArticleController : Controller
    {
        private readonly IMediator _mediator;

        public ArticleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetArticles")]
        public async Task<PageModel<ArticleModel>> GetArticlesAsync([FromBody] GetArticles getArticles)
        {
            return await _mediator.Send(getArticles);
        }

        [HttpPost("GetArticle")]
        public async Task<ArticleModel?> GetArticleAsync([FromBody] GetArticle getArticle)
        {
            return await _mediator.Send(getArticle);
        }

        [HttpPost("ExportArticles")]
        public async Task<FileResult> ExportArticlesAsync([FromBody] ExportArticles exportArticles)
        {
            var file = await _mediator.Send(exportArticles);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteArticle")]
        [HttpPost("UpdateArticle")]
        public async Task<Unit> UpdateArticleAsync([FromBody] UpdateArticle updateArticle)
        {
            return await _mediator.Send(updateArticle);
        }

        [Authorize("WriteArticle")]
        [HttpPost("CreateArticle")]
        public async Task<CreatedEntity<int>> CreateArticleAsync([FromBody] CreateArticle createArticle)
        {
            return await _mediator.Send(createArticle);
        }

        [Authorize("WriteArticle")]
        [HttpPost("DeleteArticle")]
        public async Task<Unit> DeleteArticleAsync([FromBody] DeleteArticle deleteArticle)
        {
            return await _mediator.Send(deleteArticle);
        }
    }
}