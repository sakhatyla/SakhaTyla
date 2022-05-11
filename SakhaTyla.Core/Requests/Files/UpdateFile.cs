using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using MediatR;

namespace SakhaTyla.Core.Requests.Files
{
    public class UpdateFile : IRequest
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? ContentType { get; set; }

        public Stream? Content { get; set; }
    }
}
