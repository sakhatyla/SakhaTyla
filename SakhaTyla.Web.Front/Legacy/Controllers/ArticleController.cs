using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Requests.Public.Articles;
using SakhaTyla.Core.Requests.Languages;
using SakhaTyla.Web.Front.Legacy.Models;
using AutoMapper;
using SakhaTyla.Web.Front.Infrastructure;

namespace SakhaTyla.Web.Front.Legacy.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [ValidateModel]
    [Route("api/articles")]
    public class ArticleController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ArticleController(IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("translate")]
        [PascalCaseJson]
        public async Task<TranslateModel> TranslateAsync(string query, int? fromLanguageId = null, int? toLanguageId = null)
        {
            string? fromLanguageCode = null;
            string? toLanguageCode = null;
            if (fromLanguageId != null)
            {
                var fromLanguage = await _mediator.Send(new GetLanguage() { Id = fromLanguageId.Value });
                fromLanguageCode = fromLanguage?.Code;
            }
            if (toLanguageId != null)
            {
                var toLanguage = await _mediator.Send(new GetLanguage() { Id = toLanguageId.Value });
                toLanguageCode = toLanguage?.Code;
            }
            var result = await _mediator.Send(new Translate()
            {
                Query = query,
                FromLanguageCode = fromLanguageCode,
                ToLanguageCode = toLanguageCode,
            });
            return _mapper.Map<TranslateModel>(result);
        }

        [HttpGet("suggest")]
        [PascalCaseJson]
        public async Task<List<Core.Requests.Public.Articles.Models.ArticleSuggestModel>> SuggestArticlesAsync(string query)
        {
            return await _mediator.Send(new SuggestArticles()
            {
                Query = query,
            });
        }

        [HttpGet("random")]
        [PascalCaseJson]
        public async Task<ArticleModel> GetRandomArticleAsync()
        {
            var result = await _mediator.Send(new GetRandomArticle());
            return _mapper.Map<ArticleModel>(result);
        }
    }
}
