using System;
using MediatR;

namespace SakhaTyla.Core.Requests.Articles
{
    public class ImportArticles : IRequest<Unit>
    {
        public int? FromLanguageId { get; set; }
        public int? ToLanguageId { get; set; }
        public int? FileId { get; set; }
    }
}
