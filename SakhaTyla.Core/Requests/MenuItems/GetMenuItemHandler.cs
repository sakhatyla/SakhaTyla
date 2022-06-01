using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.MenuItems.Models;

namespace SakhaTyla.Core.Requests.MenuItems
{
    public class GetMenuItemHandler : IRequestHandler<GetMenuItem, MenuItemModel?>
    {
        private readonly IEntityRepository<MenuItem> _menuItemRepository;
        private readonly IMapper _mapper;

        public GetMenuItemHandler(IEntityRepository<MenuItem> menuItemRepository,
            IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task<MenuItemModel?> Handle(GetMenuItem request, CancellationToken cancellationToken)
        {
            var menuItem = await _menuItemRepository.GetEntities()
                .Include(e => e.Menu)
                .Include(e => e.Parent)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (menuItem == null)
            {
                return null;
            }
            return _mapper.Map<MenuItem, MenuItemModel>(menuItem);
        }

    }
}
