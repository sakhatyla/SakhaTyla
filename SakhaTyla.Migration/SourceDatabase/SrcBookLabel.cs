using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Migration.SourceDatabase
{
    public class SrcBookLabel
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string Name { get; set; } = null!;

        public int PageId { get; set; }
    }
}
