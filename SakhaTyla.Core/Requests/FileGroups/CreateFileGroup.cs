using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.FileGroups
{
    public class CreateFileGroup : IRequest<CreatedEntity<int>>
    {
        public string? Name { get; set; }

        public Enums.FileGroupType? Type { get; set; }

        public string? Location { get; set; }

        public string? Accept { get; set; }
    }
}
