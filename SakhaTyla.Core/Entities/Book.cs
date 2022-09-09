using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class Book : BaseEntity
    {
        public Book(string name, string synonym)
        {
            Name = name;
            Synonym = synonym;
        }

        [Required()]
        [StringLength(100)]
        public string Name { get; set; }
                
        [Required()]
        [StringLength(100)]
        public string Synonym { get; set; }
                
        [Required()]
        public bool Hidden { get; set; }
                
        [StringLength(100)]
        public string? Cover { get; set; }

        public ICollection<BookAuthorship> Authors { get; set; } = null!;
    }
}
