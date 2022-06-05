using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SakhaTyla.Core.Requests.Comments.Models
{
    public class CommentModel
    {
        public CommentModel(string text, string textSource)
        {
            Text = text;
            TextSource = textSource;
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

        [DisplayName("TreePath")]
        public string? TreePath { get; set; }

        [DisplayName("TreeOrder")]
        public string? TreeOrder { get; set; }

        [DisplayName("Container")]
        public int ContainerId { get; set; }
        public CommentContainers.Models.CommentContainerShortModel Container { get; set; } = null!;

        [DisplayName("Text")]
        public string Text { get; set; }

        [DisplayName("Text Source")]
        public string TextSource { get; set; }

        [DisplayName("Author")]
        public int? AuthorId { get; set; }
        public Users.Models.UserShortModel? Author { get; set; }

        [DisplayName("Parent")]
        public int? ParentId { get; set; }
        public Comments.Models.CommentShortModel? Parent { get; set; }

        public ICollection<CommentModel> Children { get; set; } = null!;
    }
}
