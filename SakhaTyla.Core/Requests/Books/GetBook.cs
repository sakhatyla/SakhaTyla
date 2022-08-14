using System;
using MediatR;
using SakhaTyla.Core.Requests.Books.Models;

namespace SakhaTyla.Core.Requests.Books
{
    public class GetBook : IRequest<BookModel?>
    {
        public int? Id { get; set; }

        public string? Synonym { get; set; }
    }
}
