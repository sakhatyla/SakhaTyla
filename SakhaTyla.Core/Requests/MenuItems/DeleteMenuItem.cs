using System;
using MediatR;

namespace SakhaTyla.Core.Requests.MenuItems
{
    public class DeleteMenuItem : IRequest
    {
        public int Id { get; set; }
    }
}
