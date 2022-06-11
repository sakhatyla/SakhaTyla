using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Migration.SourceDatabase
{
    public class SrcBook
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Synonym { get; set; } = null!;

        public bool IsHidden { get; set; }

        public string? Cover { get; set; }
    }
}
