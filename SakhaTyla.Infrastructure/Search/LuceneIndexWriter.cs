using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Microsoft.Extensions.Options;

namespace SakhaTyla.Infrastructure.Search
{
    public class LuceneIndexWriter : Core.Search.ISearchIndexWriter, IDisposable
    {
        private readonly LuceneOptions _options;
        private readonly LuceneContext _luceneContext;
        private readonly IndexWriter _writer;

        public LuceneIndexWriter(IOptions<LuceneOptions> options, LuceneContext luceneContext)
        {
            _options = options.Value;
            _luceneContext = luceneContext;
            _writer = CreateWriter();
        }

        private IndexWriter CreateWriter()
        {
            var directory = FSDirectory.Open(_options.IndexPath);
            var analyzer = _luceneContext.GetAnalyzer();
            var indexConfig = new IndexWriterConfig(_options.Version, analyzer);
            return new IndexWriter(directory, indexConfig);
        }

        private Dictionary<string, string> GetFieldLanguages(Core.Search.Document document)
        {
            return document.Fields
                .Where(f => !string.IsNullOrEmpty(f.Value.Language))
                .ToDictionary(f => f.Key, f => f.Value.Language!);
        }

        public void AddDocument(Core.Search.Document document)
        {
            var analyzer = _luceneContext.GetAnalyzer(document.Language, GetFieldLanguages(document));
            _writer.AddDocument(CreateDocument(document), analyzer);
        }

        public void UpdateDocument(Core.Search.Document document)
        {
            var analyzer = _luceneContext.GetAnalyzer(document.Language, GetFieldLanguages(document));
            var term = new Term(LuceneValues.IdField, document.Id);
            _writer.UpdateDocument(term, CreateDocument(document), analyzer);
        }

        public void DeleteDocument(Core.Search.Document document)
        {
            var term = new Term(LuceneValues.IdField, document.Id);
            _writer.DeleteDocuments(term);
        }

        public int GetDocumentCount()
        {
            return _writer.NumDocs;
        }

        public void Commit()
        {
            _writer.Commit();
        }

        public void DeleteAll()
        {
            _writer.DeleteAll();
        }

        private Document CreateDocument(Core.Search.Document document)
        {
            var doc = new Document();
            doc.Add(new StringField(LuceneValues.IdField, document.Id, Field.Store.YES));
            if (!string.IsNullOrEmpty(document.Type))
            {
                doc.Add(new StringField(LuceneValues.TypeField, document.Type, Field.Store.YES));
            }
            foreach (var value in document.Fields)
            {
                var field = GetField(value.Key, value.Value);
                if (field != null)
                {
                    doc.Add(field);
                }
            }
            return doc;
        }

        private Field? GetField(string name, Core.Search.DocumentField documentField)
        {
            if (!documentField.Indexed)
            {
                return documentField.Stored ? new StoredField(name, documentField.Value) : null;
            }
            if (!documentField.Analyzed)
            {
                return new StringField(name, documentField.Value, documentField.Stored ? Field.Store.YES : Field.Store.NO);
            }            
            return new TextField(name, documentField.Value, documentField.Stored ? Field.Store.YES : Field.Store.NO);
        }

        public void Dispose()
        {
            if (_writer != null)
            {
                _writer.Dispose();
            }
        }

    }
}
