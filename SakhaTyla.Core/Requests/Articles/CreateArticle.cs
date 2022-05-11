using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Articles
{
    public class CreateArticle : IRequest<CreatedEntity<int>>
    {
        public string? Title { get; set; }

        public string? TextSource { get; set; }

        public int? FromLanguageId { get; set; }

        public int? ToLanguageId { get; set; }

        public bool? Fuzzy { get; set; }

        public int? CategoryId { get; set; }
    }
}
