using MediatR;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Pages.Models;

namespace SakhaTyla.Core.Requests.Pages
{
    public class GetPages : IRequest<PageModel<PageModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public PageFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
