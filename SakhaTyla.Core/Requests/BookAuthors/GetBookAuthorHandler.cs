using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.BookAuthors.Models;

namespace SakhaTyla.Core.Requests.BookAuthors
{
    public class GetBookAuthorHandler : IRequestHandler<GetBookAuthor, BookAuthorModel?>
    {
        private readonly IEntityRepository<BookAuthor> _bookAuthorRepository;
        private readonly IMapper _mapper;

        public GetBookAuthorHandler(IEntityRepository<BookAuthor> bookAuthorRepository,
            IMapper mapper)
        {
            _bookAuthorRepository = bookAuthorRepository;
            _mapper = mapper;
        }

        public async Task<BookAuthorModel?> Handle(GetBookAuthor request, CancellationToken cancellationToken)
        {
            var bookAuthor = await _bookAuthorRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (bookAuthor == null)
            {
                return null;
            }
            return _mapper.Map<BookAuthor, BookAuthorModel>(bookAuthor);
        }

    }
}
