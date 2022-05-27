using MediatR;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Tags.Models;

namespace SakhaTyla.Core.Requests.Tags
{
    public class GetTags : IRequest<PageModel<TagModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public TagFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
