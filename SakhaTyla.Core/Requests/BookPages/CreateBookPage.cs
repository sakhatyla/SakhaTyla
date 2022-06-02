using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.BookPages
{
    public class CreateBookPage : IRequest<CreatedEntity<int>>
    {
        public int? BookId { get; set; }

        public string? FileName { get; set; }

        public int? Number { get; set; }
    }
}
