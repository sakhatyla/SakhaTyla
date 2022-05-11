using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Languages.Models;

namespace SakhaTyla.Core.Requests.Languages
{
    public class ExportLanguages : IRequest<FileContentModel>
    {
        public LanguageFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
