using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SakhaTyla.Core.Requests.Roles.Models
{
    public class RoleModel
    {
        public RoleModel(string name, string displayName)
        {
            Name = name;
            DisplayName = displayName;
        }

        public int Id { get; set; }

        [DisplayName("Creation Date")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Modification Date")]
        public DateTime ModificationDate { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Display Name")]
        public string DisplayName { get; set; }
    }
}
