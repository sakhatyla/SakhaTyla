using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.FileGroups.Models
{
    public class FileGroupShortModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
