using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Migration.SourceDatabase
{
    public class SrcArticle
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Text { get; set; } = null!;

        public string TextSource { get; set; } = null!;

        public int FromLanguageId { get; set; }

        public string FromLanguageName { get; set; } = null!;

        public int ToLanguageId { get; set; }

        public string ToLanguageName { get; set; } = null!;

        public bool IsDeleted { get; set; }

        public bool Fuzzy { get; set; }

        public int? CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public DateTimeOffset DateCreated { get; set; }

        public DateTimeOffset DateModified { get; set; }
    }
}
