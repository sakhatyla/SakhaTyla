using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class Menu : BaseEntity
    {
        public Menu(string name, string code)
        {
            Name = name;
            Code = code;
        }

        [Required()]
        [StringLength(200)]
        public string Name { get; set; }
                
        [Required()]
        [StringLength(200)]
        public string Code { get; set; }
                
    }
}
