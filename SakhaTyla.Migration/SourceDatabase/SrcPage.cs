using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Migration.SourceDatabase
{
    public class SrcPage
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Synonym { get; set; } = null!;

        public string Contents { get; set; } = null!;

        public string ContentsSource { get; set; } = null!;

        public string? Lang { get; set; }

        public bool IsPartial { get; set; }
    }
}
