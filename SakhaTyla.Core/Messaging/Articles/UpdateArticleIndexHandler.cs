using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SakhaTyla.Core.Indexers;
using SakhaTyla.Core.Search;

namespace SakhaTyla.Core.Messaging.Articles
{
    public class UpdateArticleIndexHandler : IRequestHandler<UpdateArticleIndex>
    {
        private readonly ISearchIndexWriter _searchIndexWriter;
        private readonly ArticleIndexer _articleIndexer;

        public UpdateArticleIndexHandler(ISearchIndexWriter searchIndexWriter,
            ArticleIndexer articleIndexer)
        {
            _searchIndexWriter = searchIndexWriter;
            _articleIndexer = articleIndexer;
        }

        public async Task<Unit> Handle(UpdateArticleIndex request, CancellationToken cancellationToken)
        {
            if (request.Action == IndexAction.Add)
            {
                await _articleIndexer.IndexArticleAsync(IndexAction.Add, request.Id);
            }
            else if (request.Action == IndexAction.Update)
            {
                await _articleIndexer.IndexArticleAsync(IndexAction.Update, request.Id);
            }
            else if (request.Action == IndexAction.Delete)
            {
                await _articleIndexer.IndexArticleAsync(IndexAction.Delete, request.Id);
            }
            _searchIndexWriter.Commit();
            return Unit.Value;
        }
    }
}
