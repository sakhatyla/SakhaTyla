using System;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.BookAuthorships.Models
{
    public class BookAuthorshipFilter : EntityFilter
    {
        public int? BookId { get; set; }
        public int? AuthorId { get; set; }
        public int? WeightFrom { get; set; }
        public int? WeightTo { get; set; }
    }
}
