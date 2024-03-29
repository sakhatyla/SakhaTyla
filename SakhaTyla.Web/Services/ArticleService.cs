﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Articles;
using SakhaTyla.Core.Requests.Articles.Models;
using SakhaTyla.Web.Protos;
using SakhaTyla.Web.Protos.Articles;

namespace SakhaTyla.Web.Services
{
    [Authorize("ReadArticle")]
    public class ArticleService : Protos.Articles.ArticleService.ArticleServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ArticleService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<ArticlePageModel> GetArticles(GetArticlesRequest getArticlesRequest, ServerCallContext context)
        {
            var getArticles = _mapper.Map<GetArticlesRequest, GetArticles>(getArticlesRequest);
            var model = await _mediator.Send(getArticles);
            return _mapper.Map<PageModel<ArticleModel>, ArticlePageModel>(model);
        }

        public override async Task<Article> GetArticle(GetArticleRequest getArticleRequest, ServerCallContext context)
        {
            var getArticle = _mapper.Map<GetArticleRequest, GetArticle>(getArticleRequest);
            var model = await _mediator.Send(getArticle);
            return _mapper.Map<ArticleModel, Article>(model!);
        }

        [Authorize("WriteArticle")]
        public override async Task<Empty> UpdateArticle(UpdateArticleRequest updateArticleRequest, ServerCallContext context)
        {
            var updateArticle = _mapper.Map<UpdateArticleRequest, UpdateArticle>(updateArticleRequest);
            var model = await _mediator.Send(updateArticle);
            return _mapper.Map<Unit, Empty>(model);
        }

        [Authorize("WriteArticle")]
        public override async Task<CreatedEntity> CreateArticle(CreateArticleRequest createArticleRequest, ServerCallContext context)
        {
            var createArticle = _mapper.Map<CreateArticleRequest, CreateArticle>(createArticleRequest);
            var model = await _mediator.Send(createArticle);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(model);
        }

        [Authorize("WriteArticle")]
        public override async Task<Empty> DeleteArticle(DeleteArticleRequest deleteArticleRequest, ServerCallContext context)
        {
            var deleteArticle = _mapper.Map<DeleteArticleRequest, DeleteArticle>(deleteArticleRequest);
            var model = await _mediator.Send(deleteArticle);
            return _mapper.Map<Unit, Empty>(model);
        }
    }
}
