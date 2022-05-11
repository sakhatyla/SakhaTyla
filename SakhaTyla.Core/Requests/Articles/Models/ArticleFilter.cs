using System;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Articles.Models
{
    public class ArticleFilter : EntityFilter
    {
        public string? Title { get; set; }
        public int? FromLanguageId { get; set; }
        public int? ToLanguageId { get; set; }
        public bool? Fuzzy { get; set; }
        public int? CategoryId { get; set; }
    }
}
