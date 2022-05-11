using System;
using System.Collections.Generic;
using System.ComponentModel;
using SakhaTyla.Core.Requests.Roles.Models;

namespace SakhaTyla.Core.Requests.Users.Models
{
    public class UserModel
    {
        public UserModel(string userName, string email)
        {
            UserName = userName;
            Email = email;
        }

        public int Id { get; set; }

        [DisplayName("Creation Date")]
        public DateTime CreationDate { get; set; }

        [DisplayName("Modification Date")]
        public DateTime ModificationDate { get; set; }

        [DisplayName("UserName")]
        public string UserName { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Email Confirmed")]
        public bool EmailConfirmed { get; set; }

        public IList<int> RoleIds { get; set; } = null!;

        public IList<RoleShortModel> Roles { get; set; } = null!;

        [DisplayName("First Name")]
        public string? FirstName { get; set; }

        [DisplayName("Last Name")]
        public string? LastName { get; set; }
    }
}
