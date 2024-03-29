﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using SakhaTyla.Core.Requests.Comments.Models;
using SakhaTyla.Core.Requests.Pages.Models;

namespace SakhaTyla.Core.Requests.CommentContainers.Models
{
    public class CommentContainerModel
    {
        public CommentContainerModel()
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

        [DisplayName("Comment Count")]
        public int CommentCount { get; set; }

        public PageModel? Page { get; set; }

        public ICollection<CommentModel> Comments { get; set; } = null!;
    }
}
