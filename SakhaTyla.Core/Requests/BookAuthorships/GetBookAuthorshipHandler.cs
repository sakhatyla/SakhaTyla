using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.BookAuthorships.Models;

namespace SakhaTyla.Core.Requests.BookAuthorships
{
    public class GetBookAuthorshipHandler : IRequestHandler<GetBookAuthorship, BookAuthorshipModel?>
    {
        private readonly IEntityRepository<BookAuthorship> _bookAuthorshipRepository;
        private readonly IMapper _mapper;

        public GetBookAuthorshipHandler(IEntityRepository<BookAuthorship> bookAuthorshipRepository,
            IMapper mapper)
        {
            _bookAuthorshipRepository = bookAuthorshipRepository;
            _mapper = mapper;
        }

        public async Task<BookAuthorshipModel?> Handle(GetBookAuthorship request, CancellationToken cancellationToken)
        {
            var bookAuthorship = await _bookAuthorshipRepository.GetEntities()
                .Include(e => e.Book)
                .Include(e => e.Author)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (bookAuthorship == null)
            {
                return null;
            }
            return _mapper.Map<BookAuthorship, BookAuthorshipModel>(bookAuthorship);
        }

    }
}
