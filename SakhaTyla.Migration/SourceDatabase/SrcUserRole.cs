using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Migration.SourceDatabase
{
    public class SrcUserRole
    {
        public int UserId { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; } = null!;
    }
}
