using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.Roles.Models
{
    public class RoleShortModel
    {
        public RoleShortModel(string displayName)
        {
            DisplayName = displayName;
        }

        public int Id { get; set; }

        public string DisplayName { get; set; }

        public override string ToString()
        {
            return $"{DisplayName}";
        }
    }
}
