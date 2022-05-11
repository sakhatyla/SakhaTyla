using System;
using MediatR;
using SakhaTyla.Core.Requests.Articles.Models;

namespace SakhaTyla.Core.Requests.Articles
{
    public class GetArticle : IRequest<ArticleModel?>
    {
        public int Id { get; set; }
    }
}
