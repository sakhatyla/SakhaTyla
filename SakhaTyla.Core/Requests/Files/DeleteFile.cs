using System;
using MediatR;

namespace SakhaTyla.Core.Requests.Files
{
    public class DeleteFile : IRequest
    {
        public int Id { get; set; }
    }
}
