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

namespace SakhaTyla.Core.Requests.MenuItems
{
    public class UpdateMenuItemHandler : IRequestHandler<UpdateMenuItem>
    {
        private readonly IEntityRepository<MenuItem> _menuItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateMenuItemHandler(IEntityRepository<MenuItem> menuItemRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _menuItemRepository = menuItemRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateMenuItem request, CancellationToken cancellationToken)
        {
            var menuItem = await _menuItemRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (menuItem == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Menu Item"], request.Id]);
            }
            _mapper.Map(request, menuItem);
            await _menuItemRepository.CheckTreeLoop(menuItem);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _menuItemRepository.CalculateTree(menuItem);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
