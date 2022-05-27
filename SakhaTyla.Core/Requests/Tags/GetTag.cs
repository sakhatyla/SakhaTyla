using System;
using MediatR;
using SakhaTyla.Core.Requests.Tags.Models;

namespace SakhaTyla.Core.Requests.Tags
{
    public class GetTag : IRequest<TagModel?>
    {
        public int Id { get; set; }
    }
}
