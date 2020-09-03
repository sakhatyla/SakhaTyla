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
using SakhaTyla.Core.Requests.Categories.Models;

namespace SakhaTyla.Core.Requests.Categories
{
    public class ExportCategoriesHandler : IRequestHandler<ExportCategories, FileContentModel>
    {
        private readonly IEntityRepository<Category> _categoryRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportCategoriesHandler(IEntityRepository<Category> categoryRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportCategories request, CancellationToken cancellationToken)
        {
            IQueryable<Category> query = _categoryRepository.GetEntities();            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var categories = await query.ToListAsync();
            var models = _mapper.Map<List<Category>, List<CategoryModel>>(categories);
            return await _excelFormatter.GetExcelFileAsync(models, "Categories");
        }

    }
}
