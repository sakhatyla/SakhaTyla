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
using SakhaTyla.Core.Requests.Menus.Models;

namespace SakhaTyla.Core.Requests.Menus
{
    public class ExportMenusHandler : IRequestHandler<ExportMenus, FileContentModel>
    {
        private readonly IEntityRepository<Menu> _menuRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportMenusHandler(IEntityRepository<Menu> menuRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _menuRepository = menuRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportMenus request, CancellationToken cancellationToken)
        {
            IQueryable<Menu> query = _menuRepository.GetEntities();            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var menus = await query.ToListAsync(cancellationToken);
            var models = _mapper.Map<List<Menu>, List<MenuModel>>(menus);
            return await _excelFormatter.GetExcelFileAsync(models, "Menus");
        }

    }
}
