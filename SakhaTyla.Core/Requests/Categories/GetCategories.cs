using MediatR;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Categories.Models;

namespace SakhaTyla.Core.Requests.Categories
{
    public class GetCategories : IRequest<PageModel<CategoryModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public CategoryFilter? Filter { get; set; }
        public string? OrderBy { get; set; }
        public OrderDirection? OrderDirection { get; set; }
    }
}
