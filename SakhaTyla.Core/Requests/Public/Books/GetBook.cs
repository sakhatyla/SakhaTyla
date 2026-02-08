using MediatR;
using SakhaTyla.Core.Requests.Public.Books.Models;

namespace SakhaTyla.Core.Requests.Public.Books
{
    public class GetBook : IRequest<BookModel?>
    {
        public int? Id { get; set; }
    }
}
