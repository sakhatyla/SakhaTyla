using System;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Languages.Models
{
    public class LanguageFilter : EntityFilter
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
