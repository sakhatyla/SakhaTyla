using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Public.BookLabels.Models
{
    public class BookLabelFilter : EntityFilter
    {
        public int? BookId { get; set; }
        public string? Name { get; set; }
        public int? PageId { get; set; }
    }
}
