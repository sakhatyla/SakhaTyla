using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.MenuItems
{
    public class UpdateMenuItemWeightHandler : IRequestHandler<UpdateMenuItemWeight>
    {
        private readonly IEntityRepository<MenuItem> _menuItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateMenuItemWeightHandler(IEntityRepository<MenuItem> menuItemRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _menuItemRepository = menuItemRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateMenuItemWeight request, CancellationToken cancellationToken)
        {
            var menuItems = await _menuItemRepository.GetEntities()
                .Where(e => e.MenuId == request.MenuId && e.ParentId == request.ParentId)
                .ToListAsync();
            if (menuItems == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Элемент меню"], request.MenuId]);
            }
            var weight = request.Ids!.Length;
            foreach (var id in request.Ids)
            {
                var menuItem = menuItems.First(e => e.Id == id);
                menuItem.Weight = weight--;
            }
            await _unitOfWork.CommitAsync();
            await _menuItemRepository.CalculateTree(menuItems.First());
            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }

    }
}
