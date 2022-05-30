using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.Pages.Models
{
    public class PageShortModel
    {
        public PageShortModel(string name)
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
