using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class Comment : BaseEntity
    {
        public Comment(string textSource)
        {
            TextSource = textSource;
        }

        [Required()]
        public int ContainerId { get; set; }
        public CommentContainer Container { get; set; } = null!;
                
        [Required()]
        public string Text { get; set; } = null!;

        [Required()]
        public string TextSource { get; set; }
                

        public int? AuthorId { get; set; }
        public User? Author { get; set; }
                

        public int? ParentId { get; set; }
        public Comment? Parent { get; set; }
                

        public string? TreePath { get; set; }
                

        public string? TreeOrder { get; set; }

        public ICollection<Comment> Children { get; set; } = null!;

    }
}
