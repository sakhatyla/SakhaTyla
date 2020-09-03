using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.Categories
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategory>
    {
        private readonly IEntityRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateCategoryHandler(IEntityRepository<Category> categoryRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateCategory request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync();
            if (category == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Category"], request.Id]);
            }
            _mapper.Map(request, category);
            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }

    }
}
