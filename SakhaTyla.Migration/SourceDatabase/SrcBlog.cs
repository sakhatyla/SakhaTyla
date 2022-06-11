using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Migration.SourceDatabase
{
    public class SrcBlog
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Synonym { get; set; } = null!;
    }
}
