using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakhaTyla.Core.FileStorage
{
    public class Entry
    {
        public string Name { get; }
        public string Url { get; }
        public EntryType Type { get; }
        public string ContentType { get; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }

        public Entry(string name, string url, EntryType type, string contentType = null,
            DateTime? creationDate = null, DateTime? modificationDate = null)
        {
            Name = name;
            Url = url;
            Type = type;
            ContentType = contentType;
            CreationDate = creationDate;
            ModificationDate = modificationDate;
        }
    }

    public enum EntryType
    {
        File,
        Directory,
    }
}
