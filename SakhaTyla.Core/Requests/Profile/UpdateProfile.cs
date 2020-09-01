using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MediatR;

namespace SakhaTyla.Core.Requests.Profile
{
    public class UpdateProfile : IRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
