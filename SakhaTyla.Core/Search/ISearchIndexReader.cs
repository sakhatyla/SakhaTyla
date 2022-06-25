using System;
using System.Collections.Generic;
using System.Text;

namespace SakhaTyla.Core.Search
{
    public interface ISearchIndexReader
    {
        SearchResult Search(string query, string[] fields, int top, int offset = 0, string? type = null, SearchFilter[]? filters = null, bool matchAll = false);
    }

    public abstract class SearchFilter
    {
        protected SearchFilter(string field)
        {
            Field = field;
        }

        public string Field { get; set; }
    }

    public class ValueFilter : SearchFilter
    {
        public ValueFilter(string field, string value) : base(field)
        {
            Value = value;
        }

        public string Value { get; set; }
    }

    public class RangeFilter : SearchFilter
    {
        public RangeFilter(string field, string fromValue, string toValue) : base(field)
        {
            FromValue = fromValue;
            ToValue = toValue;
        }

        public string FromValue { get; set; }

        public string ToValue { get; set; }
    }
}
