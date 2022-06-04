using MediatR;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.BookAuthorships.Models;

namespace SakhaTyla.Core.Requests.BookAuthorships
{
    public class GetBookAuthorships : IRequest<PageModel<BookAuthorshipModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public BookAuthorshipFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
