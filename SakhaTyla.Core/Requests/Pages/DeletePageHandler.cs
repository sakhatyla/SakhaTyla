using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.Pages
{
    public class DeletePageHandler : IRequestHandler<DeletePage>
    {
        private readonly IEntityRepository<Page> _pageRepository;
        private readonly IEntityRepository<CommentContainer> _commentContainerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeletePageHandler(IEntityRepository<Page> pageRepository,
            IEntityRepository<CommentContainer> commentContainerRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> localizer)
        {
            _pageRepository = pageRepository;
            _commentContainerRepository = commentContainerRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeletePage request, CancellationToken cancellationToken)
        {
            var page = await _pageRepository.GetEntities()
                .Include(e => e.CommentContainer)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (page == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Page"], request.Id]);
            }
            _pageRepository.Delete(page);
            _commentContainerRepository.Delete(page.CommentContainer);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
