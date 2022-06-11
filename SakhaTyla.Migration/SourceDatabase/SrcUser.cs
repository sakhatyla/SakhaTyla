using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Migration.SourceDatabase
{
    public class SrcUser
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public bool EmailConfirmed { get; set; }

        public string Name { get; set; } = null!;
    }
}
