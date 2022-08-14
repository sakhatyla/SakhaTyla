using System;
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
        private readonly IEntityRepository<BookPage> _bookPageRepository;
        private readonly IMapper _mapper;

        public GetBookHandler(IEntityRepository<Book> bookRepository,
            IEntityRepository<BookPage> bookPageRepository,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _bookPageRepository = bookPageRepository;
            _mapper = mapper;
        }

        public async Task<BookModel?> Handle(GetBook request, CancellationToken cancellationToken)
        {
            IQueryable<Book> query = _bookRepository.GetEntities()
                .Include(e => e.Authors)
                .ThenInclude(e => e.Author);
            if (request.Id != null)
            {
                query = query.Where(e => e.Id == request.Id);
            }
            else if (!string.IsNullOrEmpty(request.Synonym))
            {
                query = query.Where(e => e.Synonym == request.Synonym);
            }
            else
            {
                throw new Exception("Id or Synonym is required");
            }
            var book = await query
                .FirstOrDefaultAsync(cancellationToken);
            if (book == null)
            {
                return null;
            }
            var model = _mapper.Map<Book, BookModel>(book);
            model.FirstPage = await _bookPageRepository.GetEntities()
                .Where(e => e.BookId == book.Id)
                .OrderBy(e => e.Number)
                .Select(e => e.Number)
                .FirstOrDefaultAsync();
            model.LastPage = await _bookPageRepository.GetEntities()
                .Where(e => e.BookId == book.Id)
                .OrderByDescending(e => e.Number)
                .Select(e => e.Number)
                .FirstOrDefaultAsync();
            return model;
        }

    }
}
