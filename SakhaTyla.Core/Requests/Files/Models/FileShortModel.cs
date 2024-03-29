﻿using System;
using System.Collections.Generic;

namespace SakhaTyla.Core.Requests.Files.Models
{
    public class FileShortModel
    {
        public FileShortModel(string name, string contentType)
        {
            Name = name;
            ContentType = contentType;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public string? Url { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
