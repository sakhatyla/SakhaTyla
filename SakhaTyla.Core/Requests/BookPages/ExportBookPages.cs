using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.BookPages.Models;

namespace SakhaTyla.Core.Requests.BookPages
{
    public class ExportBookPages : IRequest<FileContentModel>
    {
        public BookPageFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
