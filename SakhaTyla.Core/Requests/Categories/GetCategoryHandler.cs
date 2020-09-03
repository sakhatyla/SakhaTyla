using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Categories.Models;

namespace SakhaTyla.Core.Requests.Categories
{
    public class GetCategoryHandler : IRequestHandler<GetCategory, CategoryModel>
    {
        private readonly IEntityRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryHandler(IEntityRepository<Category> categoryRepository,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryModel> Handle(GetCategory request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync();
            return _mapper.Map<Category, CategoryModel>(category);
        }

    }
}
