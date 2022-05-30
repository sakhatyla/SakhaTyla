using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SakhaTyla.Core.Requests.Routes
{
    public class UpdateRoute : IRequest
    {
        public string? Path { get; set; }
    }
}
