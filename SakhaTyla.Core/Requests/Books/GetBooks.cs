using MediatR;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Books.Models;

namespace SakhaTyla.Core.Requests.Books
{
    public class GetBooks : IRequest<PageModel<BookModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public BookFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
