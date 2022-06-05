using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Comments.Models;

namespace SakhaTyla.Core.Requests.Comments
{
    public class ExportComments : IRequest<FileContentModel>
    {
        public CommentFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
