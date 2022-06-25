using System;
using System.Collections.Generic;
using System.Text;

namespace SakhaTyla.Core.Search
{
    public class IndexedDocument
    {
        public string? Id { get; set; }

        public string? Type { get; set; }

        public Dictionary<string, IndexedDocumentField> Fields { get; } = new Dictionary<string, IndexedDocumentField>();
    }

    public class IndexedDocumentField
    {
        public IndexedDocumentField(string value)
        {
            Value = value;
        }

        public string Value { get; set; }
    }
}
