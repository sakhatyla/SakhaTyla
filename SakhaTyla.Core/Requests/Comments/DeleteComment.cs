using System;
using MediatR;

namespace SakhaTyla.Core.Requests.Comments
{
    public class DeleteComment : IRequest
    {
        public int Id { get; set; }
    }
}
