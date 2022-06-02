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

namespace SakhaTyla.Core.Requests.BookPages
{
    public class UpdateBookPageHandler : IRequestHandler<UpdateBookPage>
    {
        private readonly IEntityRepository<BookPage> _bookPageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateBookPageHandler(IEntityRepository<BookPage> bookPageRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _bookPageRepository = bookPageRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateBookPage request, CancellationToken cancellationToken)
        {
            var bookPage = await _bookPageRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (bookPage == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Book Page"], request.Id]);
            }
            _mapper.Map(request, bookPage);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
