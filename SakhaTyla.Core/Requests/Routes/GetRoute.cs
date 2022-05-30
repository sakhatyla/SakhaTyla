using System;
using MediatR;
using SakhaTyla.Core.Requests.Routes.Models;

namespace SakhaTyla.Core.Requests.Routes
{
    public class GetRoute : IRequest<RouteModel?>
    {
        public string? Path { get; set; }
    }
}
