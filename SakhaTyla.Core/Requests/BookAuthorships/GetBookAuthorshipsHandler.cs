using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.BookAuthorships.Models;

namespace SakhaTyla.Core.Requests.BookAuthorships
{
    public class GetBookAuthorshipsHandler : IRequestHandler<GetBookAuthorships, PageModel<BookAuthorshipModel>>
    {
        private readonly IEntityRepository<BookAuthorship> _bookAuthorshipRepository;
        private readonly IMapper _mapper;

        public GetBookAuthorshipsHandler(IEntityRepository<BookAuthorship> bookAuthorshipRepository,
            IMapper mapper)
        {
            _bookAuthorshipRepository = bookAuthorshipRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<BookAuthorshipModel>> Handle(GetBookAuthorships request, CancellationToken cancellationToken)
        {
            IQueryable<BookAuthorship> query = _bookAuthorshipRepository.GetEntities()
                .Include(e => e.Book)
                .Include(e => e.Author);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var bookAuthorships = await query.ToPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);
            return bookAuthorships.Map<BookAuthorship, BookAuthorshipModel>(_mapper);
        }

    }
}
