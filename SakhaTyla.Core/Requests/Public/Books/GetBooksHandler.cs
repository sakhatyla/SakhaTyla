using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Public.Books.Models;

namespace SakhaTyla.Core.Requests.Public.Books
{
    public class GetBooksHandler : IRequestHandler<GetBooks, PageModel<BookModel>>
    {
        private readonly IEntityRepository<Book> _bookRepository;
        private readonly IMapper _mapper;

        public GetBooksHandler(IEntityRepository<Book> bookRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<BookModel>> Handle(GetBooks request, CancellationToken cancellationToken)
        {
            IQueryable<Book> query = _bookRepository.GetEntities()
                .Include(e => e.Authors)
                .ThenInclude(e => e.Author)
                .Where(e => !e.Hidden);
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var books = await query.ToPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);
            return books.Map<Book, BookModel>(_mapper);
        }

    }
}
