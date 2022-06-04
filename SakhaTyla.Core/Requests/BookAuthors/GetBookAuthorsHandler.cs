using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.BookAuthors.Models;

namespace SakhaTyla.Core.Requests.BookAuthors
{
    public class GetBookAuthorsHandler : IRequestHandler<GetBookAuthors, PageModel<BookAuthorModel>>
    {
        private readonly IEntityRepository<BookAuthor> _bookAuthorRepository;
        private readonly IMapper _mapper;

        public GetBookAuthorsHandler(IEntityRepository<BookAuthor> bookAuthorRepository,
            IMapper mapper)
        {
            _bookAuthorRepository = bookAuthorRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<BookAuthorModel>> Handle(GetBookAuthors request, CancellationToken cancellationToken)
        {
            IQueryable<BookAuthor> query = _bookAuthorRepository.GetEntities();            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var bookAuthors = await query.ToPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);
            return bookAuthors.Map<BookAuthor, BookAuthorModel>(_mapper);
        }

    }
}
