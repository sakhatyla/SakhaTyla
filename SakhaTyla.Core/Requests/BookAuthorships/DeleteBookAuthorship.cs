using System;
using MediatR;

namespace SakhaTyla.Core.Requests.BookAuthorships
{
    public class DeleteBookAuthorship : IRequest
    {
        public int Id { get; set; }
    }
}
