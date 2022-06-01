using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Books
{
    public class CreateBook : IRequest<CreatedEntity<int>>
    {
        public string? Name { get; set; }

        public string? Synonym { get; set; }

        public bool? Hidden { get; set; }

        public string? Cover { get; set; }
    }
}
