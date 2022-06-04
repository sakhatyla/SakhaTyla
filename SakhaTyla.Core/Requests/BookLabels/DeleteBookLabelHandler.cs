using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.BookLabels
{
    public class DeleteBookLabelHandler : IRequestHandler<DeleteBookLabel>
    {
        private readonly IEntityRepository<BookLabel> _bookLabelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteBookLabelHandler(IEntityRepository<BookLabel> bookLabelRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> localizer)
        {
            _bookLabelRepository = bookLabelRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeleteBookLabel request, CancellationToken cancellationToken)
        {
            var bookLabel = await _bookLabelRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (bookLabel == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Book Label"], request.Id]);
            }
            _bookLabelRepository.Delete(bookLabel);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
