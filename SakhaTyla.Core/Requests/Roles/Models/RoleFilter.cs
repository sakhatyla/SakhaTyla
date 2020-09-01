using System;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Roles.Models
{
    public class RoleFilter : EntityFilter
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
