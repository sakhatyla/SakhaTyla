using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Formatters;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.MenuItems.Models;

namespace SakhaTyla.Core.Requests.MenuItems
{
    public class ExportMenuItemsHandler : IRequestHandler<ExportMenuItems, FileContentModel>
    {
        private readonly IEntityRepository<MenuItem> _menuItemRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportMenuItemsHandler(IEntityRepository<MenuItem> menuItemRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportMenuItems request, CancellationToken cancellationToken)
        {
            IQueryable<MenuItem> query = _menuItemRepository.GetEntities()
                .Include(e => e.Menu)
                .Include(e => e.Parent);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var menuItems = await query.ToListAsync(cancellationToken);
            var models = _mapper.Map<List<MenuItem>, List<MenuItemModel>>(menuItems);
            return await _excelFormatter.GetExcelFileAsync(models, "MenuItems");
        }

    }
}
