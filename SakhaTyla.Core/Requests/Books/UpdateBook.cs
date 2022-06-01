using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SakhaTyla.Core.Requests.Books
{
    public class UpdateBook : IRequest
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Synonym { get; set; }

        public bool? Hidden { get; set; }

        public string? Cover { get; set; }
    }
}
