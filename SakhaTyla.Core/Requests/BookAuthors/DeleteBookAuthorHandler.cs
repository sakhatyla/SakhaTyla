using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.BookAuthors
{
    public class DeleteBookAuthorHandler : IRequestHandler<DeleteBookAuthor>
    {
        private readonly IEntityRepository<BookAuthor> _bookAuthorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteBookAuthorHandler(IEntityRepository<BookAuthor> bookAuthorRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> localizer)
        {
            _bookAuthorRepository = bookAuthorRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeleteBookAuthor request, CancellationToken cancellationToken)
        {
            var bookAuthor = await _bookAuthorRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (bookAuthor == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Book Author"], request.Id]);
            }
            _bookAuthorRepository.Delete(bookAuthor);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
