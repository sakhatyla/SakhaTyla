using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.MenuItems.Models;

namespace SakhaTyla.Core.Requests.MenuItems
{
    public class GetMenuItemsHandler : IRequestHandler<GetMenuItems, PageModel<MenuItemModel>>
    {
        private readonly IEntityRepository<MenuItem> _menuItemRepository;
        private readonly IMapper _mapper;

        public GetMenuItemsHandler(IEntityRepository<MenuItem> menuItemRepository,
            IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<MenuItemModel>> Handle(GetMenuItems request, CancellationToken cancellationToken)
        {
            IQueryable<MenuItem> query = _menuItemRepository.GetEntities()
                .Include(e => e.Menu)
                .Include(e => e.Parent);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var menuItems = await query.ToPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);
            return menuItems.Map<MenuItem, MenuItemModel>(_mapper);
        }

    }
}
