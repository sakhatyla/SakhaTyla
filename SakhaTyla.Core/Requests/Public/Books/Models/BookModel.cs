using System.Collections.Generic;
using System.Linq;

namespace SakhaTyla.Core.Requests.Public.Books.Models
{
    public class BookModel
    {
        public BookModel(string name, string synonym)
        {
            Name = name;
            Synonym = synonym;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Synonym { get; set; }

        public string? Cover { get; set; }

        public List<BookAuthorshipModel> Authors { get; set; } = null!;

        public string GetFullName()
        {
            return $"{Name} ({string.Join(", ", Authors.Select(a => a.Author.GetFullName()))})";
        }

        public int? FirstPage { get; set; }

        public int? LastPage { get; set; }
    }
}
