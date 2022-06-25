using System;
using System.Collections.Generic;
using System.Text;
using Lucene.Net.Util;

namespace SakhaTyla.Infrastructure.Search
{
    public class LuceneOptions
    {
        public LuceneVersion Version { get; set; }

        public string? IndexPath { get; set; }

        public string? HunspellPath { get; set; }
    }
}
