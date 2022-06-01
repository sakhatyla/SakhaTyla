using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.MenuItems
{
    public class DeleteMenuItemHandler : IRequestHandler<DeleteMenuItem>
    {
        private readonly IEntityRepository<MenuItem> _menuItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteMenuItemHandler(IEntityRepository<MenuItem> menuItemRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> localizer)
        {
            _menuItemRepository = menuItemRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeleteMenuItem request, CancellationToken cancellationToken)
        {
            var menuItem = await _menuItemRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (menuItem == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Menu Item"], request.Id]);
            }
            _menuItemRepository.Delete(menuItem);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
