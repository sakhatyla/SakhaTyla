using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.BookAuthorships
{
    public class CreateBookAuthorship : IRequest<CreatedEntity<int>>
    {
        public int? BookId { get; set; }

        public int? AuthorId { get; set; }

        public int? Weight { get; set; }
    }
}
