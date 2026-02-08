using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Public.Books.Models
{
    public class BookFilter : EntityFilter
    {
        public string? Name { get; set; }
    }
}
