using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.BookPages.Models;

namespace SakhaTyla.Core.Requests.BookPages
{
    public class GetBookPagesHandler : IRequestHandler<GetBookPages, PageModel<BookPageModel>>
    {
        private readonly IEntityRepository<BookPage> _bookPageRepository;
        private readonly IMapper _mapper;

        public GetBookPagesHandler(IEntityRepository<BookPage> bookPageRepository,
            IMapper mapper)
        {
            _bookPageRepository = bookPageRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<BookPageModel>> Handle(GetBookPages request, CancellationToken cancellationToken)
        {
            IQueryable<BookPage> query = _bookPageRepository.GetEntities()
                .Include(e => e.Book);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var bookPages = await query.ToPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);
            return bookPages.Map<BookPage, BookPageModel>(_mapper);
        }

    }
}
