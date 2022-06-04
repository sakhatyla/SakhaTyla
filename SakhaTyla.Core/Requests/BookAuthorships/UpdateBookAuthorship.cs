using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SakhaTyla.Core.Requests.BookAuthorships
{
    public class UpdateBookAuthorship : IRequest
    {
        public int Id { get; set; }

        public int? AuthorId { get; set; }

        public int? Weight { get; set; }
    }
}
