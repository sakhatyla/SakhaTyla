using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SakhaTyla.Core.Requests.Public.BookPages.Models
{
    public class BookPageModel
    {
        public BookPageModel(string fileName)
        {
            FileName = fileName;
        }

        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("File Name")]
        public string FileName { get; set; }

        [DisplayName("Number")]
        public int Number { get; set; }
    }
}
