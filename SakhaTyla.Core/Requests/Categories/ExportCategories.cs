using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Categories.Models;

namespace SakhaTyla.Core.Requests.Categories
{
    public class ExportCategories : IRequest<FileContentModel>
    {
        public CategoryFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
