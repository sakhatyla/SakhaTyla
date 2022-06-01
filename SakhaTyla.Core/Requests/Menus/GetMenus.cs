using MediatR;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Menus.Models;

namespace SakhaTyla.Core.Requests.Menus
{
    public class GetMenus : IRequest<PageModel<MenuModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public MenuFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
