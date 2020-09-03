using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Categories
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategory, CreatedEntity<int>>
    {
        private readonly IEntityRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCategoryHandler(IEntityRepository<Category> categoryRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreateCategory request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<CreateCategory, Category>(request);
            _categoryRepository.Add(category);
            await _unitOfWork.CommitAsync();
            return new CreatedEntity<int>() { Id = category.Id };
        }

    }
}
