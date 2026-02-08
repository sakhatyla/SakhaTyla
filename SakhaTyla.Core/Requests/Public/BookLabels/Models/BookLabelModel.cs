namespace SakhaTyla.Core.Requests.Public.BookLabels.Models
{
    public class BookLabelModel
    {
        public BookLabelModel(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        public int BookId { get; set; }

        public string Name { get; set; }

        public int PageId { get; set; }
    }
}
