using System;
using System.Collections.Generic;
using System.Text;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.En;
using Lucene.Net.Analysis.Miscellaneous;
using Lucene.Net.Analysis.Ru;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Util;
using Microsoft.Extensions.Options;
using SakhaTyla.Infrastructure.Search.Analyzers;

namespace SakhaTyla.Infrastructure.Search
{
    public class LuceneContext
    {
        private readonly LuceneOptions _options;

        private readonly Analyzer _defaultAnalyzer;
        private readonly Dictionary<string, Analyzer> _languageAnalyzers = new();

        public LuceneContext(IOptions<LuceneOptions> options)
        {
            _options = options.Value;
            _defaultAnalyzer = new StandardAnalyzer(_options.Version);
            _languageAnalyzers["ru"] = new RussianAnalyzer(_options.Version); // TODO: add custom stopwords
            _languageAnalyzers["en"] = new EnglishAnalyzer(_options.Version);
            _languageAnalyzers["sah"] = new SakhaAnalyzer(_options.Version, _options.HunspellPath!);
        }

        private Analyzer GetAnalyzerByLanguage(string? language)
        {
            if (!string.IsNullOrEmpty(language))
            {
                if (_languageAnalyzers.TryGetValue(language, out var analyzer))
                {
                    return analyzer;
                }
                if (language == "default")
                {
                    return _defaultAnalyzer;
                }
            }
            return _defaultAnalyzer;
        }

        public Analyzer GetAnalyzer(string? language = null, Dictionary<string, string>? fieldLanguages = null)
        {
            var analyzer = GetAnalyzerByLanguage(language);
            if (fieldLanguages != null && fieldLanguages.Count > 0)
            {
                var fieldAnalyzers = new Dictionary<string, Analyzer>();
                foreach (var fieldLanguage in fieldLanguages)
                {
                    fieldAnalyzers[fieldLanguage.Key] = GetAnalyzerByLanguage(fieldLanguage.Value);
                }
                analyzer = new PerFieldAnalyzerWrapper(analyzer, fieldAnalyzers);
            }
            return analyzer;
        }

        public IEnumerable<Analyzer> GetAnalyzers(string[]? languages)
        {
            var result = new List<Analyzer>();
            if (languages != null)
            {
                foreach (var language in languages)
                {
                    result.Add(GetAnalyzerByLanguage(language));
                }
            }
            if (result.Count == 0)
                result.Add(GetAnalyzer());
            return result;
        }
    }
}
