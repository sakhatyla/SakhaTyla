using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SakhaTyla.Core.Enums;

namespace SakhaTyla.Core.Entities
{
    public class FileGroup : BaseEntity
    {
        public FileGroup(string name, FileGroupType type)
        {
            Name = name;
            Type = type;
        }

        [Required()]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required()]
        public Enums.FileGroupType Type { get; set; }
        
        [StringLength(200)]
        public string? Location { get; set; }
        
        [StringLength(1000)]
        public string? Accept { get; set; }
        
    }
}
