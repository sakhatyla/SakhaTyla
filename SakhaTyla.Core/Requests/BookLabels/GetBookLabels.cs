using MediatR;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.BookLabels.Models;

namespace SakhaTyla.Core.Requests.BookLabels
{
    public class GetBookLabels : IRequest<PageModel<BookLabelModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public BookLabelFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
