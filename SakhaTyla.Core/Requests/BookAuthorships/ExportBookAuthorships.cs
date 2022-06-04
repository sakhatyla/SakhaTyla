using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.BookAuthorships.Models;

namespace SakhaTyla.Core.Requests.BookAuthorships
{
    public class ExportBookAuthorships : IRequest<FileContentModel>
    {
        public BookAuthorshipFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
