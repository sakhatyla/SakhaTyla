using System;
using MediatR;
using SakhaTyla.Core.Requests.Roles.Models;

namespace SakhaTyla.Core.Requests.Roles
{
    public class GetRole : IRequest<RoleModel>
    {
        public int Id { get; set; }
    }
}
