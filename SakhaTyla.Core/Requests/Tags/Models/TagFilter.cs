using System;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Tags.Models
{
    public class TagFilter : EntityFilter
    {
        public string? Name { get; set; }
    }
}
