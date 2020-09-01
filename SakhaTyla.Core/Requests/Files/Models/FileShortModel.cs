using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.Files.Models
{
    public class FileShortModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
