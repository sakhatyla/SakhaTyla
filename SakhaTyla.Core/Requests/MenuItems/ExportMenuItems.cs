using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.MenuItems.Models;

namespace SakhaTyla.Core.Requests.MenuItems
{
    public class ExportMenuItems : IRequest<FileContentModel>
    {
        public MenuItemFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
