using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Articles.Models;

namespace SakhaTyla.Core.Requests.Articles
{
    public class GetArticleHandler : IRequestHandler<GetArticle, ArticleModel>
    {
        private readonly IEntityRepository<Article> _articleRepository;
        private readonly IMapper _mapper;

        public GetArticleHandler(IEntityRepository<Article> articleRepository,
            IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
        }

        public async Task<ArticleModel> Handle(GetArticle request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetEntities()
                .Include(e => e.FromLanguage)
                .Include(e => e.ToLanguage)
                .Include(e => e.Category)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync();
            return _mapper.Map<Article, ArticleModel>(article);
        }

    }
}
