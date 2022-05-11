using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.Files.Models
{
    public class FileShortModel
    {
        public FileShortModel(string name)
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
