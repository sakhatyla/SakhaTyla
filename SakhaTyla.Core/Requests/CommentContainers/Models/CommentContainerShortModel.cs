using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.CommentContainers.Models
{
    public class CommentContainerShortModel
    {
        public CommentContainerShortModel(int commentCount)
        {
            CommentCount = commentCount;
        }

        public int Id { get; set; }

        public int CommentCount { get; set; }

        public override string ToString()
        {
            return $"{CommentCount}";
        }
    }
}
