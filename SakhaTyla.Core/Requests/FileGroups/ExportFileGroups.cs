using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.FileGroups.Models;

namespace SakhaTyla.Core.Requests.FileGroups
{
    public class ExportFileGroups : IRequest<FileContentModel>
    {
        public FileGroupFilter Filter { get; set; }
        public string OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
