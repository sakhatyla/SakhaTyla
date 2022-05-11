using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using MediatR;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Files
{
    public class CreateFile : IRequest<CreatedEntity<int>>
    {
        public string? Name { get; set; }

        public string? ContentType { get; set; }

        public Stream? Content { get; set; }

        public int? GroupId { get; set; }
    }
}
