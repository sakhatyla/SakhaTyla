using System;
using MediatR;
using SakhaTyla.Core.Requests.CommentContainers.Models;

namespace SakhaTyla.Core.Requests.CommentContainers
{
    public class GetCommentContainer : IRequest<CommentContainerModel?>
    {
        public int Id { get; set; }
    }
}
