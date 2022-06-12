using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Migration.SourceDatabase
{
    public class SrcArticleHistory
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }

        public string? NewTitle { get; set; }

        public string? NewTextSource { get; set; }

        public bool NewFuzzy { get; set; }

        public string UserCreatedEmail { get; set; } = null!;

        public DateTimeOffset DateCreated { get; set; }

        public HistoryType Type { get; set; }
    }

    public enum HistoryType
    {
        Created,
        Updated,
        Deleted
    }
}
