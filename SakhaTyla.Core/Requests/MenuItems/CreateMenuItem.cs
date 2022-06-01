using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.MenuItems
{
    public class CreateMenuItem : IRequest<CreatedEntity<int>>
    {
        public int? MenuId { get; set; }

        public string? Name { get; set; }

        public string? Url { get; set; }

        public int? ParentId { get; set; }
    }
}
