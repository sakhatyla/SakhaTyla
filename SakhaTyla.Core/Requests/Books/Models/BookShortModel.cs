using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.Books.Models
{
    public class BookShortModel
    {
        public BookShortModel(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
