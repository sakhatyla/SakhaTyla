using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SakhaTyla.Core.Requests.Articles
{
    public class UpdateArticle : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TextSource { get; set; }

        public int? FromLanguageId { get; set; }

        public int? ToLanguageId { get; set; }

        public bool? Fuzzy { get; set; }

        public int? CategoryId { get; set; }
    }
}
