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
    public class SearchArticlesByTextHandler : IRequestHandler<SearchArticlesByText, List<ArticleModel>>
    {
        private readonly ISearchIndexReader _searchIndexReader;
        private readonly IMapper _mapper;

        public SearchArticlesByTextHandler(ISearchIndexReader searchIndexReader,
            IMapper mapper)
        {
            _searchIndexReader = searchIndexReader;
            _mapper = mapper;
        }

        public Task<List<ArticleModel>> Handle(SearchArticlesByText request, CancellationToken cancellationToken)
        {
            var query = request.Query!;
            var result = _searchIndexReader.Search(query, new[] { ArticleSearch.TextSourceFromField, ArticleSearch.TextSourceToField }, 100, languages: ArticleSearch.GetLanguages(query).ToArray());
            var articles = result.Documents.Select(ArticleSearch.GetArticle).ToList();
            var models = articles.Select(e => _mapper.Map<Article, ArticleModel>(e))
                .Take(10)
                .ToList();
            // TODO: merge SearchArticlesByText and SearchArticlesByTitle into one request
            return Task.FromResult(models);
        }
    }
}
