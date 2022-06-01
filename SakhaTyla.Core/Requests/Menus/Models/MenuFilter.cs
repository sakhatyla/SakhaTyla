using System;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Menus.Models
{
    public class MenuFilter : EntityFilter
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
}
