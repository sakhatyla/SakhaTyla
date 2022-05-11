using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Categories.Models;

namespace SakhaTyla.Core.Requests.Categories
{
    public class GetCategoriesHandler : IRequestHandler<GetCategories, PageModel<CategoryModel>>
    {
        private readonly IEntityRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoriesHandler(IEntityRepository<Category> categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<CategoryModel>> Handle(GetCategories request, CancellationToken cancellationToken)
        {
            IQueryable<Category> query = _categoryRepository.GetEntities();            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var categories = await query.ToPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);
            return categories.Map<Category, CategoryModel>(_mapper);
        }

    }
}
