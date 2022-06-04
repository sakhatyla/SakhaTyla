using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.BookAuthors
{
    public class CreateBookAuthor : IRequest<CreatedEntity<int>>
    {
        public string? LastName { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? NickName { get; set; }
    }
}
