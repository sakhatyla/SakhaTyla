using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.Articles
{
    public class UpdateArticleHandler : IRequestHandler<UpdateArticle>
    {
        private readonly IEntityRepository<Article> _articleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateArticleHandler(IEntityRepository<Article> articleRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _articleRepository = articleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateArticle request, CancellationToken cancellationToken)
        {
            var article = await _articleRepository.GetEntities()
                .DefaultFilter()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync();
            if (article == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Article"], request.Id]);
            }
            _mapper.Map(request, article);
            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }

    }
}
