using MediatR;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.MenuItems.Models;

namespace SakhaTyla.Core.Requests.MenuItems
{
    public class GetMenuItems : IRequest<PageModel<MenuItemModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public MenuItemFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
