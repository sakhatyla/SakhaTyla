using System;
using System.Collections.Generic;
using System.ComponentModel;
using SakhaTyla.Core.Requests.Routes.Models;

namespace SakhaTyla.Core.Requests.Pages.Models
{
    public class PageModel
    {
        public PageModel(string name)
        {
            Name = name;
        }

        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Creation Date")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Modification Date")]
        public DateTime ModificationDate { get; set; }

        [DisplayName("Creation User")]
        public int? CreationUserId { get; set; }
        public Users.Models.UserShortModel? CreationUser { get; set; }

        [DisplayName("Modification User")]
        public int? ModificationUserId { get; set; }
        public Users.Models.UserShortModel? ModificationUser { get; set; }

        [DisplayName("Tree Path")]
        public string? TreePath { get; set; }

        [DisplayName("Tree Order")]
        public string? TreeOrder { get; set; }

        [DisplayName("Type")]
        public Enums.PageType Type { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Short Name")]
        public string? ShortName { get; set; }

        [DisplayName("Parent")]
        public int? ParentId { get; set; }
        public Pages.Models.PageShortModel? Parent { get; set; }

        [DisplayName("Header")]
        public string? Header { get; set; }

        [DisplayName("Body")]
        public string? Body { get; set; }

        [DisplayName("Meta Title")]
        public string? MetaTitle { get; set; }

        [DisplayName("Meta Keywords")]
        public string? MetaKeywords { get; set; }

        [DisplayName("Meta Description")]
        public string? MetaDescription { get; set; }

        [DisplayName("Image")]
        public int? ImageId { get; set; }
        public Files.Models.FileShortModel? Image { get; set; }

        [DisplayName("Preview")]
        public string? Preview { get; set; }

        public RouteShortModel? Route { get; set; }

        [DisplayName("Comment Container")]
        public int CommentContainerId { get; set; }
        public CommentContainers.Models.CommentContainerShortModel CommentContainer { get; set; } = null!;

        [DisplayName("Publication Date")]
        public DateTime? PublicationDate { get; set; }

        public string GetShortName()
        {
            if (!string.IsNullOrEmpty(ShortName))
                return ShortName;
            return Name;
        }

        public string GetHeader()
        {
            if (!string.IsNullOrEmpty(Header))
                return Header;
            return Name;
        }

        public string GetTitle()
        {
            if (!string.IsNullOrEmpty(MetaTitle))
                return MetaTitle;
            if (!string.IsNullOrEmpty(Header))
                return Header;
            return Name;
        }
    }
}
