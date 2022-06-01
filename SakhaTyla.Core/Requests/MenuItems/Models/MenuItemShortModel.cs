using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.MenuItems.Models
{
    public class MenuItemShortModel
    {
        public MenuItemShortModel(string name)
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
