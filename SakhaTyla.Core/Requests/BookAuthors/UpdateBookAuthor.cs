using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SakhaTyla.Core.Requests.BookAuthors
{
    public class UpdateBookAuthor : IRequest
    {
        public int Id { get; set; }

        public string? LastName { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? NickName { get; set; }
    }
}
