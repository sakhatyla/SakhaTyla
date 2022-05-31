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
using SakhaTyla.Core.Requests.Widgets.Models;

namespace SakhaTyla.Core.Requests.Widgets
{
    public class ExportWidgetsHandler : IRequestHandler<ExportWidgets, FileContentModel>
    {
        private readonly IEntityRepository<Widget> _widgetRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportWidgetsHandler(IEntityRepository<Widget> widgetRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _widgetRepository = widgetRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportWidgets request, CancellationToken cancellationToken)
        {
            IQueryable<Widget> query = _widgetRepository.GetEntities();            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var widgets = await query.ToListAsync(cancellationToken);
            var models = _mapper.Map<List<Widget>, List<WidgetModel>>(widgets);
            return await _excelFormatter.GetExcelFileAsync(models, "Widgets");
        }

    }
}
