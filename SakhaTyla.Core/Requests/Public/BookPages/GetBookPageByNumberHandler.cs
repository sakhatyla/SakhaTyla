using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Public.BookPages.Models;

namespace SakhaTyla.Core.Requests.Public.BookPages
{
    public class GetBookPageByNumberHandler : IRequestHandler<GetBookPageByNumber, BookPageModel?>
    {
        private readonly IEntityRepository<BookPage> _bookPageRepository;
        private readonly IMapper _mapper;

        public GetBookPageByNumberHandler(IEntityRepository<BookPage> bookPageRepository,
            IMapper mapper)
        {
            _bookPageRepository = bookPageRepository;
            _mapper = mapper;
        }

        public async Task<BookPageModel?> Handle(GetBookPageByNumber request, CancellationToken cancellationToken)
        {
            var bookPage = await _bookPageRepository.GetEntities()
                .Include(e => e.Book)
                .Where(e => e.Book.Synonym == request.Synonym && e.Number == request.Number)
                .FirstOrDefaultAsync(cancellationToken);
            if (bookPage == null)
            {
                return null;
            }
            return _mapper.Map<BookPage, BookPageModel>(bookPage);
        }

    }
}
