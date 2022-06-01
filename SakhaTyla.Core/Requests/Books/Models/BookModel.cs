using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SakhaTyla.Core.Requests.Books.Models
{
    public class BookModel
    {
        public BookModel(string name, string synonym)
        {
            Name = name;
            Synonym = synonym;
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

        [DisplayName("Synonym")]
        public string Synonym { get; set; }

        [DisplayName("Hidden")]
        public bool Hidden { get; set; }

        [DisplayName("Cover")]
        public string? Cover { get; set; }
    }
}
