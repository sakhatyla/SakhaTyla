using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SakhaTyla.Core.Requests.BookLabels
{
    public class UpdateBookLabel : IRequest
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int? PageId { get; set; }
    }
}
