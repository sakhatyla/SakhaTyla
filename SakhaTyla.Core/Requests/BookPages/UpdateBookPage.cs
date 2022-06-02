using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SakhaTyla.Core.Requests.BookPages
{
    public class UpdateBookPage : IRequest
    {
        public int Id { get; set; }

        public string? FileName { get; set; }

        public int? Number { get; set; }
    }
}
