using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class CommentContainer : BaseEntity
    {
        public CommentContainer()
        {
        }

        [Required()]
        public int CommentCount { get; set; }

        public Page? Page { get; set; }
    }
}
