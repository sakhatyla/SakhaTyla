using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Indexers;
using SakhaTyla.Core.Requests.Public.Articles.Models;
using SakhaTyla.Core.Search;

namespace SakhaTyla.Core.Requests.Public.Articles
{
    public class SearchArticlesByTitleHandler : IRequestHandler<SearchArticlesByTitle, List<ArticleModel>>
    {
        private readonly ISearchIndexReader _searchIndexReader;
        private readonly IMapper _mapper;

        public SearchArticlesByTitleHandler(ISearchIndexReader searchIndexReader,
            IMapper mapper)
        {
            _searchIndexReader = searchIndexReader;
            _mapper = mapper;
        }

        public Task<List<ArticleModel>> Handle(SearchArticlesByTitle request, CancellationToken cancellationToken)
        {
            var query = request.Query!;
            var filters = new List<SearchFilter>();
            if (!string.IsNullOrEmpty(request.FromLanguageCode))
            {
                filters.Add(new ValueFilter(ArticleSearch.FromLanguageCodeField, request.FromLanguageCode));
            }
            if (!string.IsNullOrEmpty(request.ToLanguageCode))
            {
                filters.Add(new ValueFilter(ArticleSearch.ToLanguageCodeField, request.ToLanguageCode));
            }
            var result = _searchIndexReader.Search(query, new[] { ArticleSearch.TitleField }, 100, filters: filters.ToArray(), languages: ArticleSearch.GetLanguages(query).ToArray());
            var articles = result.Documents.Select(ArticleSearch.GetArticle).ToList();
            var models = ExactMatchFirst(articles, query)
                .Select(e => _mapper.Map<Article, ArticleModel>(e))
                .ToList();
            // TODO: search in database if nothing found in index
            // TODO: group results by language
            return Task.FromResult(models);
        }

        private IEnumerable<Article> ExactMatchFirst(IEnumerable<Article> articles, string query)
        {
            var keywords = query.Split(' ')
                .Where(s => !string.IsNullOrEmpty(s))
                .ToArray();
            return articles.Select(a => new
            {
                Article = a,
                Order = a.Title.Split(' ')
                        .Where(s => !string.IsNullOrEmpty(s))
                        .Any(s => keywords.Any(k => string.Equals(s, k, StringComparison.CurrentCultureIgnoreCase))) ? 0 : 1,
            })
                .OrderBy(a => a.Order)
                .ThenBy(a => a.Article.Title)
                .ThenBy(a => a.Article.Id)
                .Select(a => a.Article);
        }
    }
}
