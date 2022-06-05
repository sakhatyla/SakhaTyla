using System;
using MediatR;
using SakhaTyla.Core.Requests.Comments.Models;

namespace SakhaTyla.Core.Requests.Comments
{
    public class GetComment : IRequest<CommentModel?>
    {
        public int Id { get; set; }
    }
}
