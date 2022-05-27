using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class ArticleTag : BaseEntity
    {
        public ArticleTag()
        {
        }

        [Required()]
        public int ArticleId { get; set; }
        public Article Article { get; set; } = null!;
                
        [Required()]
        public int TagId { get; set; }
        public Tag Tag { get; set; } = null!;
                
    }
}
