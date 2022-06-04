using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.BookAuthors.Models
{
    public class BookAuthorShortModel
    {
        public BookAuthorShortModel(string lastName)
        {
            LastName = lastName;
        }

        public int Id { get; set; }

        public string LastName { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName}";
        }
    }
}
