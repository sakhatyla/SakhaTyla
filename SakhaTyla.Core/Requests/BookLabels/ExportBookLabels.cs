using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.BookLabels.Models;

namespace SakhaTyla.Core.Requests.BookLabels
{
    public class ExportBookLabels : IRequest<FileContentModel>
    {
        public BookLabelFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
