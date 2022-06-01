using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Menus
{
    public class CreateMenuHandler : IRequestHandler<CreateMenu, CreatedEntity<int>>
    {
        private readonly IEntityRepository<Menu> _menuRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMenuHandler(IEntityRepository<Menu> menuRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _menuRepository = menuRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreateMenu request, CancellationToken cancellationToken)
        {
            var menu = _mapper.Map<CreateMenu, Menu>(request);
            _menuRepository.Add(menu);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new CreatedEntity<int>(menu.Id);
        }

    }
}
