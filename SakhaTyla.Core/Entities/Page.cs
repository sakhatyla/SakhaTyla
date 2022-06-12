using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class Page : BaseEntity
    {
        public Page(string name)
        {
            Name = name;
        }

        [Required()]
        public Enums.PageType Type { get; set; }
                
        [Required()]
        [StringLength(200)]
        public string Name { get; set; }
                
        [StringLength(100)]
        public string? ShortName { get; set; }
                

        public int? ParentId { get; set; }
        public Page? Parent { get; set; }
                
        [StringLength(500)]
        public string? Header { get; set; }
                

        public string? Body { get; set; }
                
        [StringLength(500)]
        public string? MetaTitle { get; set; }
                
        [StringLength(2000)]
        public string? MetaKeywords { get; set; }
                

        public string? MetaDescription { get; set; }
                
        [StringLength(1000)]
        public string? TreePath { get; set; }
                
        [StringLength(1000)]
        public string? TreeOrder { get; set; }
                

        public int? ImageId { get; set; }
        public File? Image { get; set; }
                

        public string? Preview { get; set; }

        public Route? Route { get; set; }

        [Required()]
        public int CommentContainerId { get; set; }
        public CommentContainer CommentContainer { get; set; } = null!;


        public DateTime? PublicationDate { get; set; }
                
    }
}
