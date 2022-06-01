using System;
using MediatR;

namespace SakhaTyla.Core.Requests.Books
{
    public class DeleteBook : IRequest
    {
        public int Id { get; set; }
    }
}
