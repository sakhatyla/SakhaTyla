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
using SakhaTyla.Core.Requests.Languages.Models;
using SakhaTyla.Core.Requests.Public.Articles.Models;
using SakhaTyla.Core.Search;

namespace SakhaTyla.Core.Requests.Public.Articles
{
    public class TranslateHandler : IRequestHandler<Translate, TranslateModel>
    {
        private readonly ISearchIndexReader _searchIndexReader;
        private readonly IMapper _mapper;

        public TranslateHandler(ISearchIndexReader searchIndexReader,
            IMapper mapper)
        {
            _searchIndexReader = searchIndexReader;
            _mapper = mapper;
        }

        public Task<TranslateModel> Handle(Translate request, CancellationToken cancellationToken)
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
            var articleGroups = models.GroupBy(a => new { FromLanguageName = a.FromLanguage.Name, ToLanguageName = a.ToLanguage.Name })
                .Select(a => new ArticleGroupModel()
                {
                    FromLanguage = new LanguageShortModel(a.Key.FromLanguageName),
                    ToLanguage = new LanguageShortModel(a.Key.ToLanguageName),
                    Articles = a.ToList(),
                })
                .ToList();
            var model = new TranslateModel(query)
            {
                Articles = articleGroups,
            };
            result = _searchIndexReader.Search(query, new[] { ArticleSearch.TextSourceFromField, ArticleSearch.TextSourceToField }, 100, languages: ArticleSearch.GetLanguages(query).ToArray());
            var moreArticles = result.Documents.Select(ArticleSearch.GetArticle).ToList();
            model.MoreArticles = moreArticles.Select(e => _mapper.Map<Article, ArticleModel>(e))
                .Where(e => models.All(a => a.Id != e.Id))
                .Take(10)
                .ToList();
            return Task.FromResult(model);
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
