using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cynosura.Core.Data;
using Microsoft.EntityFrameworkCore;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Search;

namespace SakhaTyla.Core.Indexers
{
    public class ArticleIndexer
    {
        private readonly ISearchIndexWriter _searchIndexWriter;
        private readonly IEntityRepository<Article> _articleRepository;

        public ArticleIndexer(ISearchIndexWriter searchIndexWriter, 
            IEntityRepository<Article> articleRepository)
        {
            _searchIndexWriter = searchIndexWriter;
            _articleRepository = articleRepository;
        }

        public async Task IndexArticlesAsync()
        {
            await IndexArticlesAsync(IndexAction.Add);
        }

        private async Task IndexArticlesAsync(IndexAction action)
        {
            var articles = await _articleRepository.GetEntities()
                .Include(e => e.FromLanguage)
                .Include(e => e.ToLanguage)
                .Include(e => e.Category)
                .Where(e => !e.IsDeleted)
                .ToListAsync();
            foreach (var article in articles)
            {
                IndexArticleInner(action, article);
            }
        }

        public async Task IndexArticleAsync(IndexAction action, int articleId)
        {
            var article = await _articleRepository.GetEntities()
                .Include(e => e.FromLanguage)
                .Include(e => e.ToLanguage)
                .Include(e => e.Category)
                .Where(e => e.Id == articleId)
                .FirstAsync();
            IndexArticleInner(action, article);
        }

        private void IndexArticleInner(IndexAction action, Article article)
        {
            var document = ArticleSearch.GetDocument(article);
            if (action == IndexAction.Add)
            {
                _searchIndexWriter.AddDocument(document);
            }
            else if (action == IndexAction.Update)
            {
                _searchIndexWriter.UpdateDocument(document);
            }
            else if (action == IndexAction.Delete)
            {
                _searchIndexWriter.DeleteDocument(document);
            }
        }
    }
}
