using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class WorkerInfo : BaseEntity
    {
        [Required()]
        [StringLength(200)]
        public string Name { get; set; }
        
        [Required()]
        [StringLength(200)]
        public string ClassName { get; set; }
        
    }
}
