using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class BookLabel : BaseEntity
    {
        public BookLabel(string name)
        {
            Name = name;
        }

        [Required()]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
                
        [Required()]
        [StringLength(200)]
        public string Name { get; set; }
                
        [Required()]
        public int PageId { get; set; }
        public BookPage Page { get; set; } = null!;
                
    }
}
