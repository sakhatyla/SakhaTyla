using System;
using MediatR;

namespace SakhaTyla.Core.Requests.Tags
{
    public class DeleteTag : IRequest
    {
        public int Id { get; set; }
    }
}
