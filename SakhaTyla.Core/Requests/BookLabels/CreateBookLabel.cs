using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.BookLabels
{
    public class CreateBookLabel : IRequest<CreatedEntity<int>>
    {
        public int? BookId { get; set; }

        public string? Name { get; set; }

        public int? PageId { get; set; }
    }
}
