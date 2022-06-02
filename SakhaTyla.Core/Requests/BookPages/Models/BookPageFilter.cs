using System;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.BookPages.Models
{
    public class BookPageFilter : EntityFilter
    {
        public int? BookId { get; set; }
        public string? FileName { get; set; }
        public int? NumberFrom { get; set; }
        public int? NumberTo { get; set; }
    }
}
