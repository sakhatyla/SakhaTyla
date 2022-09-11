using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Util;

namespace SakhaTyla.Infrastructure.Search.Analyzers
{
    public class StandardAnalyzer : Analyzer
    {
        protected readonly LuceneVersion MatchVersion;

        public StandardAnalyzer(LuceneVersion matchVersion)
        {
            MatchVersion = matchVersion;
        }

        protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
        {
            Tokenizer tokenizer = new StandardTokenizer(MatchVersion, reader);
            TokenFilter filter = new LowerCaseFilter(MatchVersion, tokenizer);
            filter = new ReplaceYoFilter(filter);
            return new TokenStreamComponents(tokenizer, filter);
        }
    }
}
