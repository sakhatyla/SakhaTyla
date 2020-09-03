using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.Articles
{
    public class DeleteArticleHandler : IRequestHandler<DeleteArticle>
    {
        private readonly IEntityRepository<Article> _articleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteArticleHandler(IEntityRepository<Article> articleRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> localizer)
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeleteArticle request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync();
            if (article == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Article"], request.Id]);
            }
            _articleRepository.Delete(article);
            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }

    }
}
