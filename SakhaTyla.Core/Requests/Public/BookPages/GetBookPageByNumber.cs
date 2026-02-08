using System;
using MediatR;
using SakhaTyla.Core.Requests.Public.BookPages.Models;

namespace SakhaTyla.Core.Requests.Public.BookPages
{
    public class GetBookPageByNumber : IRequest<BookPageModel?>
    {
        public int? Id { get; set; }

        public string? Synonym { get; set; }

        public int Number { get; set; }
    }
}
