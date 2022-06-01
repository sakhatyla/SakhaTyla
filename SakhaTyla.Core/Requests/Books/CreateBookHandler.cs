using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Books
{
    public class CreateBookHandler : IRequestHandler<CreateBook, CreatedEntity<int>>
    {
        private readonly IEntityRepository<Book> _bookRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBookHandler(IEntityRepository<Book> bookRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreateBook request, CancellationToken cancellationToken)
        {
            var book = _mapper.Map<CreateBook, Book>(request);
            _bookRepository.Add(book);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new CreatedEntity<int>(book.Id);
        }

    }
}
