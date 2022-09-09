using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cynosura.Core.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Public.Articles.Models;

namespace SakhaTyla.Core.Requests.Public.Articles
{
    public class SuggestArticlesHandler : IRequestHandler<SuggestArticles, List<ArticleSuggestModel>>
    {
        private readonly IEntityRepository<Article> _articleRepository;

        public SuggestArticlesHandler(IEntityRepository<Article> articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public async Task<List<ArticleSuggestModel>> Handle(SuggestArticles request, CancellationToken cancellationToken)
        {
            if (request.Query!.Length < SuggestArticles.MinLength)
            {
                return new List<ArticleSuggestModel>();
            }
            var suggestions = await _articleRepository.GetEntities()
                .Where(a => a.Title.StartsWith(request.Query))
                .GroupBy(a => a.Title)
                .Take(10)
                .Select(g => g.Select(a => new ArticleSuggestModel()
                {
                    Id = a.Id,
                    Title = a.Title,
                }).First())
                .ToListAsync();
            return suggestions;
        }
    }
}
