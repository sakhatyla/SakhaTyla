using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Public.Articles.Models;

namespace SakhaTyla.Core.Requests.Public.Articles
{
    public class GetRandomArticleHandler : IRequestHandler<GetRandomArticle, ArticleModel>
    {
        private readonly IEntityRepository<Article> _articleRepository;
        private readonly IMapper _mapper;

        public GetRandomArticleHandler(IEntityRepository<Article> articleRepository,
            IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<ArticleModel> Handle(GetRandomArticle request, CancellationToken cancellationToken)
        {
            var count = await _articleRepository.GetEntities().CountAsync();
            var rnd = new Random();
            var i = rnd.Next(count);
            var article = await _articleRepository.GetEntities()
                .Include(e => e.FromLanguage)
                .Include(e => e.ToLanguage)
                .Include(e => e.Category)
                .OrderBy(a => a.Id)
                .Skip(i)
                .FirstAsync();
            return _mapper.Map<ArticleModel>(article);
        }
    }
}
