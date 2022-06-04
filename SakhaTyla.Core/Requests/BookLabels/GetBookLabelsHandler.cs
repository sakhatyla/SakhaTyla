using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.BookLabels.Models;

namespace SakhaTyla.Core.Requests.BookLabels
{
    public class GetBookLabelsHandler : IRequestHandler<GetBookLabels, PageModel<BookLabelModel>>
    {
        private readonly IEntityRepository<BookLabel> _bookLabelRepository;
        private readonly IMapper _mapper;

        public GetBookLabelsHandler(IEntityRepository<BookLabel> bookLabelRepository,
            IMapper mapper)
        {
            _bookLabelRepository = bookLabelRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<BookLabelModel>> Handle(GetBookLabels request, CancellationToken cancellationToken)
        {
            IQueryable<BookLabel> query = _bookLabelRepository.GetEntities()
                .Include(e => e.Book)
                .Include(e => e.Page);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var bookLabels = await query.ToPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);
            return bookLabels.Map<BookLabel, BookLabelModel>(_mapper);
        }

    }
}
