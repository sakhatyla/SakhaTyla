using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SakhaTyla.Core.Requests.FileGroups
{
    public class UpdateFileGroup : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Enums.FileGroupType? Type { get; set; }

        public string Location { get; set; }

        public string Accept { get; set; }
    }
}
