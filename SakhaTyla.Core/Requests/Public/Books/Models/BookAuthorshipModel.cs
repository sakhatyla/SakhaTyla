namespace SakhaTyla.Core.Requests.Public.Books.Models
{
    public class BookAuthorshipModel
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public BookAuthorModel Author { get; set; } = null!;

        public int Weight { get; set; }
    }
}
