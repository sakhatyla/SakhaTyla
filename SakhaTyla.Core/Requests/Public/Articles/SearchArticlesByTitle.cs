using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SakhaTyla.Core.Requests.Public.Articles.Models;

namespace SakhaTyla.Core.Requests.Public.Articles
{
    public class SearchArticlesByTitle : IRequest<List<ArticleModel>>
    {
        public string? Query { get; set; }

        public string? FromLanguageCode { get; set; }

        public string? ToLanguageCode { get; set; }
    }
}
