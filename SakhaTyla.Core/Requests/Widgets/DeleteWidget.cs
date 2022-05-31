using System;
using MediatR;

namespace SakhaTyla.Core.Requests.Widgets
{
    public class DeleteWidget : IRequest
    {
        public int Id { get; set; }
    }
}
