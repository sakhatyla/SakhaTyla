using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SakhaTyla.Core.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role(string displayName)
        {
            DisplayName = displayName;
        }

        [Required()]
        public DateTime CreationDate { get; set; }
        
        [Required()]
        public DateTime ModificationDate { get; set; }
        
        [Required()]
        [StringLength(100)]
        public string DisplayName { get; set; }

        public ICollection<User> Users { get; set; } = null!;

    }
}
