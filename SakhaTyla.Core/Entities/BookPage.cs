using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class BookPage : BaseEntity
    {
        public BookPage(string fileName)
        {
            FileName = fileName;
        }

        [Required()]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
                
        [Required()]
        [StringLength(200)]
        public string FileName { get; set; }
                
        [Required()]
        public int Number { get; set; }
                
    }
}
