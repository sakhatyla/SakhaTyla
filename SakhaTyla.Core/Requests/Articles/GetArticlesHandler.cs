using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Articles.Models;

namespace SakhaTyla.Core.Requests.Articles
{
    public class GetArticlesHandler : IRequestHandler<GetArticles, PageModel<ArticleModel>>
    {
        private readonly IEntityRepository<Article> _articleRepository;
        private readonly IMapper _mapper;

        public GetArticlesHandler(IEntityRepository<Article> articleRepository,
            IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<ArticleModel>> Handle(GetArticles request, CancellationToken cancellationToken)
        {
            IQueryable<Article> query = _articleRepository.GetEntities()
                .Include(e => e.FromLanguage)
                .Include(e => e.ToLanguage)
                .Include(e => e.Category)
                .DefaultFilter();            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var articles = await query.ToPagedListAsync(request.PageIndex, request.PageSize);
            return articles.Map<Article, ArticleModel>(_mapper);
        }

    }
}
