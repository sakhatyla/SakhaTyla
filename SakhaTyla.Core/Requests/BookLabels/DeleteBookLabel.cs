using System;
using MediatR;

namespace SakhaTyla.Core.Requests.BookLabels
{
    public class DeleteBookLabel : IRequest
    {
        public int Id { get; set; }
    }
}
