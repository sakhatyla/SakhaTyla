using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.Articles.Models
{
    public class ArticleShortModel
    {
        public ArticleShortModel(string title)
        {
            Title = title;
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public override string ToString()
        {
            return $"{Title}";
        }
    }
}
