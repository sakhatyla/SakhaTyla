using System;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Widgets.Models
{
    public class WidgetFilter : EntityFilter
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Body { get; set; }
        public Enums.WidgetType? Type { get; set; }
    }
}
