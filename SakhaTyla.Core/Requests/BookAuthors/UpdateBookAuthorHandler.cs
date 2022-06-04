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

namespace SakhaTyla.Core.Requests.BookAuthors
{
    public class UpdateBookAuthorHandler : IRequestHandler<UpdateBookAuthor>
    {
        private readonly IEntityRepository<BookAuthor> _bookAuthorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateBookAuthorHandler(IEntityRepository<BookAuthor> bookAuthorRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _bookAuthorRepository = bookAuthorRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateBookAuthor request, CancellationToken cancellationToken)
        {
            var bookAuthor = await _bookAuthorRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (bookAuthor == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Book Author"], request.Id]);
            }
            _mapper.Map(request, bookAuthor);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
