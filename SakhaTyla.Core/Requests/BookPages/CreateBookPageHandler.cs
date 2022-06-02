using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.BookPages
{
    public class CreateBookPageHandler : IRequestHandler<CreateBookPage, CreatedEntity<int>>
    {
        private readonly IEntityRepository<BookPage> _bookPageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBookPageHandler(IEntityRepository<BookPage> bookPageRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _bookPageRepository = bookPageRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreateBookPage request, CancellationToken cancellationToken)
        {
            var bookPage = _mapper.Map<CreateBookPage, BookPage>(request);
            _bookPageRepository.Add(bookPage);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new CreatedEntity<int>(bookPage.Id);
        }

    }
}
