using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SakhaTyla.Core.Requests.Files.Models
{
    public class FileModel
    {
        public FileModel(string name, string contentType, int groupId)
        {
            Name = name;
            ContentType = contentType;
            GroupId = groupId;
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

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Content Type")]
        public string ContentType { get; set; }

        [DisplayName("Url")]
        public string? Url { get; set; }

        [DisplayName("Group")]
        public int GroupId { get; set; }
        public FileGroups.Models.FileGroupShortModel Group { get; set; } = null!;
    }
}
