using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.Categories.Models
{
    public class CategoryShortModel
    {
        public CategoryShortModel(string name)
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
