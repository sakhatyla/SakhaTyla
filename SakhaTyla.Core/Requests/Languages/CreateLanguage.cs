using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Languages
{
    public class CreateLanguage : IRequest<CreatedEntity<int>>
    {
        public string? Name { get; set; }

        public string? Code { get; set; }
    }
}
