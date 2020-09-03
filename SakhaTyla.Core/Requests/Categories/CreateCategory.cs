using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MediatR;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Categories
{
    public class CreateCategory : IRequest<CreatedEntity<int>>
    {
        public string Name { get; set; }
    }
}
