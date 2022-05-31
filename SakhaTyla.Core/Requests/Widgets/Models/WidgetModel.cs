using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SakhaTyla.Core.Requests.Widgets.Models
{
    public class WidgetModel
    {
        public WidgetModel(string name, string code)
        {
            Name = name;
            Code = code;
        }

        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Creation Date")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Modification Date")]
        public DateTime ModificationDate { get; set; }

        [DisplayName("Creation User")]
        public int? CreationUserId { get; set; }
        public Users.Models.UserShortModel? CreationUser { get; set; }

        [DisplayName("Modification User")]
        public int? ModificationUserId { get; set; }
        public Users.Models.UserShortModel? ModificationUser { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Code")]
        public string Code { get; set; }

        [DisplayName("Body")]
        public string? Body { get; set; }

        [DisplayName("Type")]
        public Enums.WidgetType Type { get; set; }
    }
}
