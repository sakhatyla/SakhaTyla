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
using SakhaTyla.Core.Requests.BookLabels.Models;

namespace SakhaTyla.Core.Requests.BookLabels
{
    public class ExportBookLabelsHandler : IRequestHandler<ExportBookLabels, FileContentModel>
    {
        private readonly IEntityRepository<BookLabel> _bookLabelRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportBookLabelsHandler(IEntityRepository<BookLabel> bookLabelRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _bookLabelRepository = bookLabelRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportBookLabels request, CancellationToken cancellationToken)
        {
            IQueryable<BookLabel> query = _bookLabelRepository.GetEntities()
                .Include(e => e.Book)
                .Include(e => e.Page);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var bookLabels = await query.ToListAsync(cancellationToken);
            var models = _mapper.Map<List<BookLabel>, List<BookLabelModel>>(bookLabels);
            return await _excelFormatter.GetExcelFileAsync(models, "BookLabels");
        }

    }
}
