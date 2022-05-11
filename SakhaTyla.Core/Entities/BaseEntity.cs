using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public abstract class BaseEntity
    {
        [Required()]
        public int Id { get; set; }
        
        [Required()]
        public DateTime CreationDate { get; set; }
        
        [Required()]
        public DateTime ModificationDate { get; set; }
        

        public int? CreationUserId { get; set; }
        public User? CreationUser { get; set; }
        

        public int? ModificationUserId { get; set; }
        public User? ModificationUser { get; set; }
        
    }
}
