using System;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Comments.Models
{
    public class CommentFilter : EntityFilter
    {
        public int? ContainerId { get; set; }
        public string? TextSource { get; set; }
        public int? AuthorId { get; set; }
        public int? ParentId { get; set; }
    }
}
