using System;
using MediatR;
using SakhaTyla.Core.Requests.Users.Models;

namespace SakhaTyla.Core.Requests.Users
{
    public class GetUser : IRequest<UserModel>
    {
        public int Id { get; set; }
    }
}
