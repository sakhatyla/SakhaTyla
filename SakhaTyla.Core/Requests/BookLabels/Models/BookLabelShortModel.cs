using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.BookLabels.Models
{
    public class BookLabelShortModel
    {
        public BookLabelShortModel(string name)
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
