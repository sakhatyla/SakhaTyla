using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Core;
using Lucene.Net.Analysis.Hunspell;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Util;

namespace SakhaTyla.Infrastructure.Search.Analyzers
{
    public abstract class HunspellAnalyzer : Analyzer
    {
        protected readonly LuceneVersion MatchVersion;
        private readonly string _hunspellPath;
        private readonly Dictionary _dictionary;

        public HunspellAnalyzer(LuceneVersion matchVersion, string language, string hunspellPath)
        {
            MatchVersion = matchVersion;
            _hunspellPath = hunspellPath;
            _dictionary = GetDictionary(language);
        }

        private Dictionary GetDictionary(string language)
        {
            using (var affixStream = OpenStream(language + @".aff"))
            using (var wordStream = OpenStream(language + @".dic"))
            {
                return new Dictionary(affixStream, wordStream);
            }
        }

        private Stream OpenStream(string fileName)
        {
            return new FileStream(Path.Combine(_hunspellPath, fileName), FileMode.Open, FileAccess.Read);
        }

        protected override TokenStreamComponents CreateComponents(string fieldName, TextReader reader)
        {
            Tokenizer tokenizer = new StandardTokenizer(MatchVersion, reader);
            TokenFilter filter = new StandardFilter(MatchVersion, tokenizer);
            filter = new LowerCaseFilter(MatchVersion, filter);
            filter = new HunspellStemFilter(filter, _dictionary);
            return new TokenStreamComponents(tokenizer, filter);
        }
    }
}
