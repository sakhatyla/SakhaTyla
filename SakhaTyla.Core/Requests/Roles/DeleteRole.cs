using System;
using MediatR;

namespace SakhaTyla.Core.Requests.Roles
{
    public class DeleteRole : IRequest
    {
        public int Id { get; set; }
    }
}
