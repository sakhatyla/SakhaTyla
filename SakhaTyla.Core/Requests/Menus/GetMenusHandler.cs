using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Menus.Models;

namespace SakhaTyla.Core.Requests.Menus
{
    public class GetMenusHandler : IRequestHandler<GetMenus, PageModel<MenuModel>>
    {
        private readonly IEntityRepository<Menu> _menuRepository;
        private readonly IMapper _mapper;

        public GetMenusHandler(IEntityRepository<Menu> menuRepository,
            IMapper mapper)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<MenuModel>> Handle(GetMenus request, CancellationToken cancellationToken)
        {
            IQueryable<Menu> query = _menuRepository.GetEntities();            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var menus = await query.ToPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);
            return menus.Map<Menu, MenuModel>(_mapper);
        }

    }
}
