using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Comments
{
    public class CreateComment : IRequest<CreatedEntity<int>>
    {
        public int? ContainerId { get; set; }

        public string? TextSource { get; set; }

        public int? AuthorId { get; set; }

        public int? ParentId { get; set; }
    }
}
