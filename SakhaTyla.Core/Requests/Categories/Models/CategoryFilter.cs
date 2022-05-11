using System;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Categories.Models
{
    public class CategoryFilter : EntityFilter
    {
        public string? Name { get; set; }
    }
}
