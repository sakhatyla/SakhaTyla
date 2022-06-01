using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.MenuItems
{
    public class CreateMenuItemHandler : IRequestHandler<CreateMenuItem, CreatedEntity<int>>
    {
        private readonly IEntityRepository<MenuItem> _menuItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMenuItemHandler(IEntityRepository<MenuItem> menuItemRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreateMenuItem request, CancellationToken cancellationToken)
        {
            var menuItem = _mapper.Map<CreateMenuItem, MenuItem>(request);
            _menuItemRepository.Add(menuItem);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _menuItemRepository.CalculateTree(menuItem);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new CreatedEntity<int>(menuItem.Id);
        }

    }
}
