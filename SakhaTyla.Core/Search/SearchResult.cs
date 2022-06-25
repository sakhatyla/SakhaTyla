using System;
using System.Collections.Generic;
using System.Text;

namespace SakhaTyla.Core.Search
{
    public class SearchResult
    {
        public SearchResult(IList<IndexedDocument> documents, int total)
        {
            Documents = documents;
            Total = total;
        }

        public IList<IndexedDocument> Documents { get; set; }
        public int Total { get; set; }
    }
}
