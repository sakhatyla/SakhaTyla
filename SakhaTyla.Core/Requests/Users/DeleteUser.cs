using System;
using MediatR;

namespace SakhaTyla.Core.Requests.Users
{
    public class DeleteUser : IRequest
    {
        public int Id { get; set; }
    }
}
