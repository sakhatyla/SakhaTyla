using MediatR;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Widgets.Models;

namespace SakhaTyla.Core.Requests.Widgets
{
    public class GetWidgets : IRequest<PageModel<WidgetModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public WidgetFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
