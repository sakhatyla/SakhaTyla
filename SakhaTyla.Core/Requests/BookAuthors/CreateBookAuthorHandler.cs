using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.BookAuthors
{
    public class CreateBookAuthorHandler : IRequestHandler<CreateBookAuthor, CreatedEntity<int>>
    {
        private readonly IEntityRepository<BookAuthor> _bookAuthorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBookAuthorHandler(IEntityRepository<BookAuthor> bookAuthorRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _bookAuthorRepository = bookAuthorRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreateBookAuthor request, CancellationToken cancellationToken)
        {
            var bookAuthor = _mapper.Map<CreateBookAuthor, BookAuthor>(request);
            _bookAuthorRepository.Add(bookAuthor);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new CreatedEntity<int>(bookAuthor.Id);
        }

    }
}
