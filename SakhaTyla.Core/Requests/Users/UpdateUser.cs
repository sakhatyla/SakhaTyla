using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SakhaTyla.Core.Requests.Users
{
    public class UpdateUser : IRequest
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public List<int> RoleIds { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
