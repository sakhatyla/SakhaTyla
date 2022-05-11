using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SakhaTyla.Core.Entities
{
    public class User : IdentityUser<int>
    {
        [Required()]
        public DateTime CreationDate { get; set; }
        
        [Required()]
        public DateTime ModificationDate { get; set; }
        
        [StringLength(200)]
        public string? FirstName { get; set; }
        
        [StringLength(200)]
        public string? LastName { get; set; }

        public ICollection<Role> Roles { get; set; } = null!;
        
    }
}
