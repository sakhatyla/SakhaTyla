using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Pages.Models;

namespace SakhaTyla.Core.Requests.Pages
{
    public class GetPageHandler : IRequestHandler<GetPage, PageModel?>
    {
        private readonly IEntityRepository<Page> _pageRepository;
        private readonly IMapper _mapper;

        public GetPageHandler(IEntityRepository<Page> pageRepository,
            IMapper mapper)
        {
            _pageRepository = pageRepository;
            _mapper = mapper;
        }

        public async Task<PageModel?> Handle(GetPage request, CancellationToken cancellationToken)
        {
            var page = await _pageRepository.GetEntities()
                .Include(e => e.Parent)
                .Include(e => e.Route)
                .Include(e => e.Image)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (page == null)
            {
                return null;
            }
            return _mapper.Map<Page, PageModel>(page);
        }

    }
}
