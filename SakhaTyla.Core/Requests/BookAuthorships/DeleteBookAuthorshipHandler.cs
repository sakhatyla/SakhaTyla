using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.BookAuthorships
{
    public class DeleteBookAuthorshipHandler : IRequestHandler<DeleteBookAuthorship>
    {
        private readonly IEntityRepository<BookAuthorship> _bookAuthorshipRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteBookAuthorshipHandler(IEntityRepository<BookAuthorship> bookAuthorshipRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> localizer)
        {
            _bookAuthorshipRepository = bookAuthorshipRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeleteBookAuthorship request, CancellationToken cancellationToken)
        {
            var bookAuthorship = await _bookAuthorshipRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (bookAuthorship == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Book Author"], request.Id]);
            }
            _bookAuthorshipRepository.Delete(bookAuthorship);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
