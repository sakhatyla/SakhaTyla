using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Books.Models;

namespace SakhaTyla.Core.Requests.Books
{
    public class GetBookHandler : IRequestHandler<GetBook, BookModel?>
    {
        private readonly IEntityRepository<Book> _bookRepository;
        private readonly IMapper _mapper;

        public GetBookHandler(IEntityRepository<Book> bookRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookModel?> Handle(GetBook request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (book == null)
            {
                return null;
            }
            return _mapper.Map<Book, BookModel>(book);
        }

    }
}
