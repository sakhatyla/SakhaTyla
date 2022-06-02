using System;
using MediatR;
using SakhaTyla.Core.Requests.BookPages.Models;

namespace SakhaTyla.Core.Requests.BookPages
{
    public class GetBookPage : IRequest<BookPageModel?>
    {
        public int Id { get; set; }
    }
}
