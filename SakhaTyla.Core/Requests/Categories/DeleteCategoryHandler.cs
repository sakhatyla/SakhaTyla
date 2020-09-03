using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.Categories
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategory>
    {
        private readonly IEntityRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteCategoryHandler(IEntityRepository<Category> categoryRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> localizer)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeleteCategory request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync();
            if (category == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Category"], request.Id]);
            }
            _categoryRepository.Delete(category);
            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }

    }
}
