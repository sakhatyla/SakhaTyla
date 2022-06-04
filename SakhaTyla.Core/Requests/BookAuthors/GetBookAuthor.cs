using System;
using MediatR;
using SakhaTyla.Core.Requests.BookAuthors.Models;

namespace SakhaTyla.Core.Requests.BookAuthors
{
    public class GetBookAuthor : IRequest<BookAuthorModel?>
    {
        public int Id { get; set; }
    }
}
