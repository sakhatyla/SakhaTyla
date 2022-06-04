using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class BookAuthor : BaseEntity
    {
        public BookAuthor(string lastName)
        {
            LastName = lastName;
        }

        [Required()]
        [StringLength(50)]
        public string LastName { get; set; }
                
        [StringLength(50)]
        public string? FirstName { get; set; }
                
        [StringLength(50)]
        public string? MiddleName { get; set; }
                
        [StringLength(50)]
        public string? NickName { get; set; }
                
    }
}
