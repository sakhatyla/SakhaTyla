using System;
using MediatR;

namespace SakhaTyla.Core.Requests.Articles
{
    public class DeleteArticle : IRequest
    {
        public int Id { get; set; }
    }
}
