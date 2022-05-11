using MediatR;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Files.Models;

namespace SakhaTyla.Core.Requests.Files
{
    public class GetFiles : IRequest<PageModel<FileModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public FileFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
