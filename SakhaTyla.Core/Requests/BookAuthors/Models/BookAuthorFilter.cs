using System;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.BookAuthors.Models
{
    public class BookAuthorFilter : EntityFilter
    {
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? NickName { get; set; }
    }
}
