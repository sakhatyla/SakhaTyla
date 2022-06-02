using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.BookPages
{
    public class DeleteBookPageHandler : IRequestHandler<DeleteBookPage>
    {
        private readonly IEntityRepository<BookPage> _bookPageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteBookPageHandler(IEntityRepository<BookPage> bookPageRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> localizer)
        {
            _bookPageRepository = bookPageRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeleteBookPage request, CancellationToken cancellationToken)
        {
            var bookPage = await _bookPageRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (bookPage == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Book Page"], request.Id]);
            }
            _bookPageRepository.Delete(bookPage);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
