using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.Languages.Models
{
    public class LanguageShortModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
