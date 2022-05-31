using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.Widgets.Models
{
    public class WidgetShortModel
    {
        public WidgetShortModel(string name)
        {
            Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
