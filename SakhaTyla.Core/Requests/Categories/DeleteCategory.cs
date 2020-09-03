using System;
using MediatR;

namespace SakhaTyla.Core.Requests.Categories
{
    public class DeleteCategory : IRequest
    {
        public int Id { get; set; }
    }
}
