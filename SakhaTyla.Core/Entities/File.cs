using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class File : BaseEntity
    {
        public File(string name, string contentType, int groupId)
        {
            Name = name;
            ContentType = contentType;
            GroupId = groupId;
        }

        [Required()]
        [StringLength(100)]
        public string Name { get; set; }
        
        [Required()]
        [StringLength(200)]
        public string ContentType { get; set; }
        

        public byte[]? Content { get; set; }
        
        [StringLength(200)]
        public string? Url { get; set; }
        
        [Required()]
        public int GroupId { get; set; }
        public FileGroup Group { get; set; } = null!;
        
    }
}
