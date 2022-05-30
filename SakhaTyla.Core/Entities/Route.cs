using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class Route : BaseEntity
    {
        public Route(string path)
        {
            Path = path;
        }

        [Required()]
        [StringLength(500)]
        public string Path { get; set; }
                

        public int? PageId { get; set; }
        public Page? Page { get; set; }
                
    }
}
