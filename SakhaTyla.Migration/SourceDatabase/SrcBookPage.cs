using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Migration.SourceDatabase
{
    public class SrcBookPage
    {
        public int Id { get; set; }

        public int BookId { get; set; }

        public string FileName { get; set; } = null!;

        public int Number { get; set; }
    }
}
