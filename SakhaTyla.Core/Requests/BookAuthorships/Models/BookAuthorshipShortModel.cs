using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.BookAuthorships.Models
{
    public class BookAuthorshipShortModel
    {
        public BookAuthorshipShortModel(int bookId)
        {
            BookId = bookId;
        }

        public int Id { get; set; }

        public int BookId { get; set; }

        public override string ToString()
        {
            return $"{BookId}";
        }
    }
}
