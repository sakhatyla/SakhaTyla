using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Articles.Models;

namespace SakhaTyla.Core.Requests.Articles
{
    public class ExportArticles : IRequest<FileContentModel>
    {
        public ArticleFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
