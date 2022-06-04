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

namespace SakhaTyla.Core.Requests.BookAuthorships
{
    public class UpdateBookAuthorshipHandler : IRequestHandler<UpdateBookAuthorship>
    {
        private readonly IEntityRepository<BookAuthorship> _bookAuthorshipRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateBookAuthorshipHandler(IEntityRepository<BookAuthorship> bookAuthorshipRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _bookAuthorshipRepository = bookAuthorshipRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateBookAuthorship request, CancellationToken cancellationToken)
        {
            var bookAuthorship = await _bookAuthorshipRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (bookAuthorship == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Book Author"], request.Id]);
            }
            _mapper.Map(request, bookAuthorship);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
