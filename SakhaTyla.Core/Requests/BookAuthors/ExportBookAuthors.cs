using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.BookAuthors.Models;

namespace SakhaTyla.Core.Requests.BookAuthors
{
    public class ExportBookAuthors : IRequest<FileContentModel>
    {
        public BookAuthorFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
