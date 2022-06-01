using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class MenuItem : BaseEntity
    {
        public MenuItem(string name)
        {
            Name = name;
        }

        [Required()]
        public int MenuId { get; set; }
        public Menu Menu { get; set; } = null!;
                
        [Required()]
        [StringLength(200)]
        public string Name { get; set; }
                
        [StringLength(200)]
        public string? Url { get; set; }
                
        [Required()]
        public int Weight { get; set; }
                

        public int? ParentId { get; set; }
        public MenuItem? Parent { get; set; }
                

        public string? TreePath { get; set; }
                

        public string? TreeOrder { get; set; }

        public IList<MenuItem> Children { get; set; } = null!;

    }
}
