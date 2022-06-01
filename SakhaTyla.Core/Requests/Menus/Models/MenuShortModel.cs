﻿using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.Menus.Models
{
    public class MenuShortModel
    {
        public MenuShortModel(string name)
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
