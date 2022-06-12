using System;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Pages.Models
{
    public class PageFilter : EntityFilter
    {
        public Enums.PageType? Type { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }
        public int? ParentId { get; set; }
        public string? Header { get; set; }
        public string? Body { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaKeywords { get; set; }
        public string? MetaDescription { get; set; }
        public int? ImageId { get; set; }
        public string? Preview { get; set; }
        public int? CommentContainerId { get; set; }
        public DateTime? PublicationDateFrom { get; set; }
        public DateTime? PublicationDateTo { get; set; }
    }
}
