using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.BookPages.Models;

namespace SakhaTyla.Core.Requests.BookPages
{
    public class GetBookPageHandler : IRequestHandler<GetBookPage, BookPageModel?>
    {
        private readonly IEntityRepository<BookPage> _bookPageRepository;
        private readonly IMapper _mapper;

        public GetBookPageHandler(IEntityRepository<BookPage> bookPageRepository,
            IMapper mapper)
        {
            _bookPageRepository = bookPageRepository;
            _mapper = mapper;
        }

        public async Task<BookPageModel?> Handle(GetBookPage request, CancellationToken cancellationToken)
        {
            var bookPage = await _bookPageRepository.GetEntities()
                .Include(e => e.Book)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (bookPage == null)
            {
                return null;
            }
            return _mapper.Map<BookPage, BookPageModel>(bookPage);
        }

    }
}
