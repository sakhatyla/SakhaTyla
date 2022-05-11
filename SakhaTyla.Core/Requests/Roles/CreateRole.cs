using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Roles
{
    public class CreateRole : IRequest<CreatedEntity<int>>
    {
        public string? Name { get; set; }

        public string? DisplayName { get; set; }
    }
}
