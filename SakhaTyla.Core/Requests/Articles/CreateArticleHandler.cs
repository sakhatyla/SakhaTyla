using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Articles
{
    public class CreateArticleHandler : IRequestHandler<CreateArticle, CreatedEntity<int>>
    {
        private readonly IEntityRepository<Article> _articleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateArticleHandler(IEntityRepository<Article> articleRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreateArticle request, CancellationToken cancellationToken)
        {
            var article = _mapper.Map<CreateArticle, Article>(request);
            _articleRepository.Add(article);
            await _unitOfWork.CommitAsync();
            return new CreatedEntity<int>() { Id = article.Id };
        }

    }
}
