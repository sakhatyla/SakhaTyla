using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Cynosura.Core.Data;
using Cynosura.Core.Messaging;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Indexers;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Messaging.Articles;

namespace SakhaTyla.Core.Requests.Articles
{
    public class CreateArticleHandler : IRequestHandler<CreateArticle, CreatedEntity<int>>
    {
        private readonly IEntityRepository<Article> _articleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMessagingService _messagingService;

        public CreateArticleHandler(IEntityRepository<Article> articleRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMessagingService messagingService)
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _messagingService = messagingService;
        }

        public async Task<CreatedEntity<int>> Handle(CreateArticle request, CancellationToken cancellationToken)
        {
            var article = _mapper.Map<CreateArticle, Article>(request);
            _articleRepository.Add(article);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _messagingService.SendAsync(UpdateArticleIndex.QueueName, new UpdateArticleIndex() { Id = article.Id, Action = IndexAction.Add });
            return new CreatedEntity<int>(article.Id);
        }

    }
}
