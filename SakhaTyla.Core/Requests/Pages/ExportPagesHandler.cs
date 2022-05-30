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
using SakhaTyla.Core.Requests.Pages.Models;

namespace SakhaTyla.Core.Requests.Pages
{
    public class ExportPagesHandler : IRequestHandler<ExportPages, FileContentModel>
    {
        private readonly IEntityRepository<Page> _pageRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportPagesHandler(IEntityRepository<Page> pageRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _pageRepository = pageRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportPages request, CancellationToken cancellationToken)
        {
            IQueryable<Page> query = _pageRepository.GetEntities()
                .Include(e => e.Parent)
                .Include(e => e.Route)
                .Include(e => e.Image);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var pages = await query.ToListAsync(cancellationToken);
            var models = _mapper.Map<List<Page>, List<PageModel>>(pages);
            return await _excelFormatter.GetExcelFileAsync(models, "Pages");
        }

    }
}
