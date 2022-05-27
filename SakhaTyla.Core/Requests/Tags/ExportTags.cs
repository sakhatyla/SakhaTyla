using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Tags.Models;

namespace SakhaTyla.Core.Requests.Tags
{
    public class ExportTags : IRequest<FileContentModel>
    {
        public TagFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
