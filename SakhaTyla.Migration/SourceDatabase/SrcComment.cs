using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Migration.SourceDatabase
{
    public class SrcComment
    {
        public int Id { get; set; }

        public string UserEmail { get; set; } = null!;

        public string Text { get; set; } = null!;

        public string TextSource { get; set; } = null!;

        public string PostSynonym { get; set; } = null!;

        public string BlogSynonym { get; set; } = null!;

        public DateTimeOffset DateCreated { get; set; }

        public DateTimeOffset DateModified { get; set; }
    }
}
