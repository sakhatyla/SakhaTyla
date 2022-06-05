using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SakhaTyla.Core.Requests.Comments
{
    public class UpdateComment : IRequest
    {
        public int Id { get; set; }

        public string? TextSource { get; set; }
    }
}
