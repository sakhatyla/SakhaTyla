using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Util;
using Lucene.Net.Util;

namespace SakhaTyla.Infrastructure.Search.Analyzers
{
    public class RussianAnalyzer : HunspellAnalyzer
    {
        public RussianAnalyzer(LuceneVersion matchVersion, CharArraySet stopwords, string hunspellPath)
            : base(matchVersion, stopwords, "ru_RU", hunspellPath)
        {
        }        
    }
}
