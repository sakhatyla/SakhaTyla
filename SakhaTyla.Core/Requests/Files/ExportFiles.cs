using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Files.Models;

namespace SakhaTyla.Core.Requests.Files
{
    public class ExportFiles : IRequest<FileContentModel>
    {
        public FileFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
