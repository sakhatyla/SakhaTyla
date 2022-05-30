using System;
using MediatR;

namespace SakhaTyla.Core.Requests.Pages
{
    public class DeletePage : IRequest
    {
        public int Id { get; set; }
    }
}
