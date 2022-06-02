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
using SakhaTyla.Core.Requests.BookPages.Models;

namespace SakhaTyla.Core.Requests.BookPages
{
    public class ExportBookPagesHandler : IRequestHandler<ExportBookPages, FileContentModel>
    {
        private readonly IEntityRepository<BookPage> _bookPageRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportBookPagesHandler(IEntityRepository<BookPage> bookPageRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _bookPageRepository = bookPageRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportBookPages request, CancellationToken cancellationToken)
        {
            IQueryable<BookPage> query = _bookPageRepository.GetEntities()
                .Include(e => e.Book);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var bookPages = await query.ToListAsync(cancellationToken);
            var models = _mapper.Map<List<BookPage>, List<BookPageModel>>(bookPages);
            return await _excelFormatter.GetExcelFileAsync(models, "BookPages");
        }

    }
}
