using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.Comments.Models
{
    public class CommentShortModel
    {
        public CommentShortModel(string text)
        {
            Text = text;
        }

        public int Id { get; set; }

        public string Text { get; set; }

        public override string ToString()
        {
            return $"{Text}";
        }
    }
}
