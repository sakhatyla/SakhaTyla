using System;
using MediatR;

namespace SakhaTyla.Core.Requests.FileGroups
{
    public class DeleteFileGroup : IRequest
    {
        public int Id { get; set; }
    }
}
