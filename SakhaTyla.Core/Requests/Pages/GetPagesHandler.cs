using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Pages.Models;

namespace SakhaTyla.Core.Requests.Pages
{
    public class GetPagesHandler : IRequestHandler<GetPages, PageModel<PageModel>>
    {
        private readonly IEntityRepository<Page> _pageRepository;
        private readonly IMapper _mapper;

        public GetPagesHandler(IEntityRepository<Page> pageRepository,
            IMapper mapper)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<PageModel>> Handle(GetPages request, CancellationToken cancellationToken)
        {
            IQueryable<Page> query = _pageRepository.GetEntities()
                .Include(e => e.Parent)
                .Include(e => e.Route)
                .Include(e => e.Image)
                .Include(e => e.CommentContainer);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var pages = await query.ToPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);
            return pages.Map<Page, PageModel>(_mapper);
        }

    }
}
