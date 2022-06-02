using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SakhaTyla.Core.Requests.BookPages.Models
{
    public class BookPageModel
    {
        public BookPageModel(string fileName)
        {
            FileName = fileName;
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

        [DisplayName("Book")]
        public int BookId { get; set; }
        public Books.Models.BookShortModel Book { get; set; } = null!;

        [DisplayName("File Name")]
        public string FileName { get; set; }

        [DisplayName("Number")]
        public int Number { get; set; }
    }
}
