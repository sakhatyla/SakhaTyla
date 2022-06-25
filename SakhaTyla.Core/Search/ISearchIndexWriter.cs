using System;
using System.Collections.Generic;
using System.Text;

namespace SakhaTyla.Core.Search
{
    public interface ISearchIndexWriter
    {
        void AddDocument(Document document);

        void UpdateDocument(Document document);

        void DeleteDocument(Document document);

        int GetDocumentCount();

        void Commit();

        void DeleteAll();
    }
}
