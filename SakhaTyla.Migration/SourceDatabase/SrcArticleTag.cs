using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Migration.SourceDatabase
{
    public class SrcArticleTag
    {
        public int ArticleId { get; set; }

        public int TagId { get; set; }

        public string TagName { get; set; } = null!;
    }
}
