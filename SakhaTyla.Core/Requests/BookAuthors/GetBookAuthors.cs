using MediatR;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.BookAuthors.Models;

namespace SakhaTyla.Core.Requests.BookAuthors
{
    public class GetBookAuthors : IRequest<PageModel<BookAuthorModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public BookAuthorFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
