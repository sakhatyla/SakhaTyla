using System;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Books.Models
{
    public class BookFilter : EntityFilter
    {
        public string? Name { get; set; }
        public string? Synonym { get; set; }
        public bool? Hidden { get; set; }
        public string? Cover { get; set; }
    }
}
