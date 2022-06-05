using MediatR;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Comments.Models;

namespace SakhaTyla.Core.Requests.Comments
{
    public class GetComments : IRequest<PageModel<CommentModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public CommentFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
        public bool? SkipChildren { get; set; }
    }
}
