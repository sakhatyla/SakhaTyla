using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.BookPages.Models
{
    public class BookPageShortModel
    {
        public BookPageShortModel(string fileName)
        {
            FileName = fileName;
        }

        public int Id { get; set; }

        public string FileName { get; set; }

        public override string ToString()
        {
            return $"{FileName}";
        }
    }
}
