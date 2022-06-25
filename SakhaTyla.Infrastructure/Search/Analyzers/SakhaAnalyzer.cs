using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Util;

namespace SakhaTyla.Infrastructure.Search.Analyzers
{
    public class SakhaAnalyzer : HunspellAnalyzer
    {
        public SakhaAnalyzer(LuceneVersion matchVersion, string hunspellPath)
            : base(matchVersion, "sah", hunspellPath)
        {
        }        
    }
}
