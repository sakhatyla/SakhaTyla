using System;
using MediatR;
using SakhaTyla.Core.Requests.BookPages.Models;

namespace SakhaTyla.Core.Requests.BookPages
{
    public class GetBookPageByNumber : IRequest<BookPageModel?>
    {
        public int BookId { get; set; }

        public int Number { get; set; }
    }
}
