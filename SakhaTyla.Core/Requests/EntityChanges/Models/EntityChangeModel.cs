using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SakhaTyla.Core.Requests.EntityChanges.Models
{
    public class EntityChangeModel
    {
        public int Id { get; set; }

        [DisplayName("Action")]
        public Enums.ChangeAction Action { get; set; }

        [DisplayName("From")]
        public string From { get; set; }

        [DisplayName("To")]
        public string To { get; set; }

        [DisplayName("Creation Date")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Creation User")]
        public int? CreationUserId { get; set; }
        public Users.Models.UserShortModel CreationUser { get; set; }

        public IList<EntityPropertyChange> Changes { get; set; }
    }
}
