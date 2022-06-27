using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Queries;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Microsoft.Extensions.Options;
using SakhaTyla.Core.Search;

namespace SakhaTyla.Infrastructure.Search
{
    public class LuceneIndexReader : Core.Search.ISearchIndexReader, IDisposable
    {
        private readonly LuceneOptions _options;
        private readonly LuceneContext _luceneContext;
        private readonly DirectoryReader _reader;

        public LuceneIndexReader(IOptions<LuceneOptions> options, LuceneContext luceneContext)
        {
            _options = options.Value;
            _reader = CreateReader();
            _luceneContext = luceneContext;
        }

        private DirectoryReader CreateReader()
        {
            var directory = FSDirectory.Open(_options.IndexPath);
            return DirectoryReader.Open(directory);
        }

        public SearchResult Search(string query, string[] fields, int top, int offset = 0, string? type = null, SearchFilter[]? filters = null, bool matchAll = false, string[]? languages = null)
        {
            var searcher = new IndexSearcher(_reader);
            var q = BuildQuery(query, fields, matchAll, languages);
            var topDocs = searcher.Search(q, GetFilter(type, filters), offset + top);
            var hits = topDocs.ScoreDocs.Skip(offset);
            var documents = new List<IndexedDocument>();
            foreach (var hit in hits)
            {
                var doc = searcher.Doc(hit.Doc);
                var document = new IndexedDocument();
                foreach (var docField in doc)
                {
                    if (docField.Name == LuceneValues.IdField)
                    {
                        document.Id = docField.GetStringValue();
                    }
                    else if (docField.Name == LuceneValues.TypeField)
                    {
                        document.Type = docField.GetStringValue();
                    }
                    else
                    {
                        document.Fields.Add(docField.Name, new IndexedDocumentField(docField.GetStringValue()));
                    }
                }
                documents.Add(document);
            }
            return new SearchResult(documents, topDocs.TotalHits);
        }

        private Query BuildQuery(string query, string[] fields, bool matchAll, string[]? languages)
        {
            query = QueryParserBase.Escape(query);
            var queries = _luceneContext.GetAnalyzers(languages)
                .Select(a =>
                {
                    var parser = new MultiFieldQueryParser(_options.Version, fields, a);
                    if (matchAll)
                    {
                        parser.DefaultOperator = Operator.AND;
                    }
                    return parser;
                })
                .Select(p => p.Parse(query))
                .ToList();
            return queries.Count > 1 ? CombineQueries(queries, Occur.SHOULD) : queries.First();
        }

        private Query CombineQueries(IEnumerable<Query> queries, Occur occur)
        {
            var combined = new BooleanQuery();
            foreach (var query in queries)
            {
                combined.Add(query, occur);
            }
            return combined;
        }        

        private Filter? GetFilter(string? type, SearchFilter[]? filters)
        {
            var filterList = new List<Filter>();
            if (!string.IsNullOrEmpty(type))
            {
                var typeTerm = new Term(LuceneValues.TypeField, type);
                filterList.Add(new TermFilter(typeTerm));
            }
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (filter is ValueFilter valueFilter)
                    {
                        var term = new Term(valueFilter.Field, valueFilter.Value);
                        filterList.Add(new TermFilter(term));
                    }
                    else if (filter is RangeFilter rangeFilter)
                    {
                        var stringRangeFilter = FieldCacheRangeFilter.NewStringRange(rangeFilter.Field,
                            lowerVal: rangeFilter.FromValue, includeLower: true,
                            upperVal: rangeFilter.ToValue, includeUpper: true);
                        filterList.Add(stringRangeFilter);
                    }
                }
            }
            if (filterList.Count == 0)
            {
                return null;
            }
            else if (filterList.Count == 1)
            {
                return filterList[0];
            }
            else
            {
                var booleanFilter = new BooleanFilter();
                foreach (var filter in filterList)
                {
                    booleanFilter.Add(filter, Occur.MUST);
                }
                return booleanFilter;
            }
        }

        public void Dispose()
        {
            if (_reader != null)
            {
                _reader.Dispose();
            }
        }
    }
}
