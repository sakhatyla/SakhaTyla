using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Menus.Models;

namespace SakhaTyla.Core.Requests.Menus
{
    public class ExportMenus : IRequest<FileContentModel>
    {
        public MenuFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
