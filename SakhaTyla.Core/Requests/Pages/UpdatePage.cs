using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SakhaTyla.Core.Requests.Routes;

namespace SakhaTyla.Core.Requests.Pages
{
    public class UpdatePage : IRequest
    {
        public int Id { get; set; }

        public Enums.PageType? Type { get; set; }

        public string? Name { get; set; }

        public string? ShortName { get; set; }

        public int? ParentId { get; set; }

        public string? Header { get; set; }

        public string? Body { get; set; }

        public string? MetaTitle { get; set; }

        public string? MetaKeywords { get; set; }

        public string? MetaDescription { get; set; }

        public int? ImageId { get; set; }

        public string? Preview { get; set; }

        public UpdateRoute? Route { get; set; }

        public DateTime? PublicationDate { get; set; }
    }
}
