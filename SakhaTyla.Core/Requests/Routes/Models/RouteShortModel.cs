using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.Routes.Models
{
    public class RouteShortModel
    {
        public RouteShortModel(string path)
        {
            Path = path;
        }

        public int Id { get; set; }

        public string Path { get; set; }

        public override string ToString()
        {
            return $"{Path}";
        }
    }
}
