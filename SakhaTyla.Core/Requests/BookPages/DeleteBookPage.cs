using System;
using MediatR;

namespace SakhaTyla.Core.Requests.BookPages
{
    public class DeleteBookPage : IRequest
    {
        public int Id { get; set; }
    }
}
