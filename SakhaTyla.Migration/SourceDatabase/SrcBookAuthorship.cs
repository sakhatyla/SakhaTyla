using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Migration.SourceDatabase
{
    public class SrcBookAuthorship
    {
        public int BookId { get; set; }

        public int AuthorId { get; set; }

        public int Order { get; set; }
    }
}
