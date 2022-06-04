using System;
using MediatR;
using SakhaTyla.Core.Requests.BookAuthorships.Models;

namespace SakhaTyla.Core.Requests.BookAuthorships
{
    public class GetBookAuthorship : IRequest<BookAuthorshipModel?>
    {
        public int Id { get; set; }
    }
}
