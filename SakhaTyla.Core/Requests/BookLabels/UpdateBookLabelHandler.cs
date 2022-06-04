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

namespace SakhaTyla.Core.Requests.BookLabels
{
    public class UpdateBookLabelHandler : IRequestHandler<UpdateBookLabel>
    {
        private readonly IEntityRepository<BookLabel> _bookLabelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateBookLabelHandler(IEntityRepository<BookLabel> bookLabelRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _bookLabelRepository = bookLabelRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateBookLabel request, CancellationToken cancellationToken)
        {
            var bookLabel = await _bookLabelRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (bookLabel == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Book Label"], request.Id]);
            }
            _mapper.Map(request, bookLabel);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
