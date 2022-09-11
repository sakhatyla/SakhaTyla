using System;
using System.Collections.Generic;
using System.Text;

namespace SakhaTyla.Core.Search
{
    public class Document
    {
        public const string DefaultLanguage = "default";

        public Document(string id)
        {
            Id = id;
        }

        public string Id { get; set; }

        public string? Type { get; set; }

        public string? Language { get; set; }

        public Dictionary<string, DocumentField> Fields { get; } = new Dictionary<string, DocumentField>();
    }

    public class DocumentField
    {
        public DocumentField(string value, bool stored = true, bool indexed = true, bool analyzed = true)
        {
            Value = value;
            Stored = stored;
            Indexed = indexed;
            Analyzed = analyzed;
        }

        public string Value { get; set; }

        public bool Stored { get; set; } = true;

        public bool Indexed { get; set; } = true;

        public bool Analyzed { get; set; } = true;

        public string? Language { get; set; }
    }
}
