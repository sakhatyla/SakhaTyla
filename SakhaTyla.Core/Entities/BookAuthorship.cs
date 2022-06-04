using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class BookAuthorship : BaseEntity
    {
        public BookAuthorship()
        {
        }

        [Required()]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;
                
        [Required()]
        public int AuthorId { get; set; }
        public BookAuthor Author { get; set; } = null!;
                
        [Required()]
        public int Weight { get; set; }
                
    }
}
