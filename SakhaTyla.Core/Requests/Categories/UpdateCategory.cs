using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SakhaTyla.Core.Requests.Categories
{
    public class UpdateCategory : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
