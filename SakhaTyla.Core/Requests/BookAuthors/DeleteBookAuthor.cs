using System;
using MediatR;

namespace SakhaTyla.Core.Requests.BookAuthors
{
    public class DeleteBookAuthor : IRequest
    {
        public int Id { get; set; }
    }
}
