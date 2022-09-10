using System;
using System.Collections.Generic;
using SakhaTyla.Core.Requests.Pages.Models;

namespace SakhaTyla.Core.Requests.CommentContainers.Models
{
    public class CommentContainerShortModel
    {
        public CommentContainerShortModel(int commentCount)
        {
            CommentCount = commentCount;
        }

        public int Id { get; set; }

        public PageShortModel? Page { get; set; }

        public int CommentCount { get; set; }

        public override string ToString()
        {
            return $"{CommentCount}";
        }
    }
}
