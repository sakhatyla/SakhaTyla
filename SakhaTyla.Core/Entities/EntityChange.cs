using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SakhaTyla.Core.Entities
{
    public class EntityChange
    {
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string EntityName { get; set; }

        public int EntityId { get; set; }

        [Required]
        public Enums.ChangeAction Action { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public DateTime CreationDate { get; set; }

        public User CreationUser { get; set; }

        public int? CreationUserId { get; set; }
    }
}
