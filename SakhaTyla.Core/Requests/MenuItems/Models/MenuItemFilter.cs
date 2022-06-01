using System;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.MenuItems.Models
{
    public class MenuItemFilter : EntityFilter
    {
        public int? MenuId { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public int? WeightFrom { get; set; }
        public int? WeightTo { get; set; }
        public int? ParentId { get; set; }
    }
}
