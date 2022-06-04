using System;
using MediatR;
using SakhaTyla.Core.Requests.BookLabels.Models;

namespace SakhaTyla.Core.Requests.BookLabels
{
    public class GetBookLabel : IRequest<BookLabelModel?>
    {
        public int Id { get; set; }
    }
}
