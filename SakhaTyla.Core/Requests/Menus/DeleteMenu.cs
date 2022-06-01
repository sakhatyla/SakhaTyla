using System;
using MediatR;

namespace SakhaTyla.Core.Requests.Menus
{
    public class DeleteMenu : IRequest
    {
        public int Id { get; set; }
    }
}
