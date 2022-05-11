using System;
using System.Collections.Generic;
using System.Text;

namespace SakhaTyla.Core.Infrastructure
{
    public class FileContentModel
    {
        public FileContentModel(string name, byte[] content, string contentType)
        {
            Name = name;
            Content = content;
            ContentType = contentType;
        }

        public string Name { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}
