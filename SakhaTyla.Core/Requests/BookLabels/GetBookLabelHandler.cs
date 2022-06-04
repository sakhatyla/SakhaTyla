using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.BookLabels.Models;

namespace SakhaTyla.Core.Requests.BookLabels
{
    public class GetBookLabelHandler : IRequestHandler<GetBookLabel, BookLabelModel?>
    {
        private readonly IEntityRepository<BookLabel> _bookLabelRepository;
        private readonly IMapper _mapper;

        public GetBookLabelHandler(IEntityRepository<BookLabel> bookLabelRepository,
            IMapper mapper)
        {
            _bookLabelRepository = bookLabelRepository;
            _mapper = mapper;
        }

        public async Task<BookLabelModel?> Handle(GetBookLabel request, CancellationToken cancellationToken)
        {
            var bookLabel = await _bookLabelRepository.GetEntities()
                .Include(e => e.Book)
                .Include(e => e.Page)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (bookLabel == null)
            {
                return null;
            }
            return _mapper.Map<BookLabel, BookLabelModel>(bookLabel);
        }

    }
}
