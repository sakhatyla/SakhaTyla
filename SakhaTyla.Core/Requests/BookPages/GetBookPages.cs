using MediatR;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.BookPages.Models;

namespace SakhaTyla.Core.Requests.BookPages
{
    public class GetBookPages : IRequest<PageModel<BookPageModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public BookPageFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
