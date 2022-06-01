using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Menus
{
    public class CreateMenu : IRequest<CreatedEntity<int>>
    {
        public string? Name { get; set; }

        public string? Code { get; set; }
    }
}
