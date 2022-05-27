using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SakhaTyla.Core.Requests.ArticleTags.Models
{
    public class ArticleTagModel
    {
        public ArticleTagModel()
        {
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

        [DisplayName("Article")]
        public int ArticleId { get; set; }
        public Articles.Models.ArticleShortModel Article { get; set; } = null!;

        [DisplayName("Tag")]
        public int TagId { get; set; }
        public Tags.Models.TagShortModel Tag { get; set; } = null!;
    }
}
