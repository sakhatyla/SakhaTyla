using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class Language : BaseEntity
    {
        [Required()]
        [StringLength(50)]
        public string Name { get; set; }
        
        [Required()]
        [StringLength(10)]
        public string Code { get; set; }
        
    }
}
