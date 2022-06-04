using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.BookAuthorships
{
    public class CreateBookAuthorshipHandler : IRequestHandler<CreateBookAuthorship, CreatedEntity<int>>
    {
        private readonly IEntityRepository<BookAuthorship> _bookAuthorshipRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBookAuthorshipHandler(IEntityRepository<BookAuthorship> bookAuthorshipRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _bookAuthorshipRepository = bookAuthorshipRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreateBookAuthorship request, CancellationToken cancellationToken)
        {
            var bookAuthorship = _mapper.Map<CreateBookAuthorship, BookAuthorship>(request);
            _bookAuthorshipRepository.Add(bookAuthorship);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new CreatedEntity<int>(bookAuthorship.Id);
        }

    }
}
