using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SakhaTyla.Core.Requests.Menus
{
    public class UpdateMenu : IRequest
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Code { get; set; }
    }
}
