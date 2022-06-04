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
using SakhaTyla.Core.Requests.BookAuthorships.Models;

namespace SakhaTyla.Core.Requests.BookAuthorships
{
    public class ExportBookAuthorshipsHandler : IRequestHandler<ExportBookAuthorships, FileContentModel>
    {
        private readonly IEntityRepository<BookAuthorship> _bookAuthorshipRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportBookAuthorshipsHandler(IEntityRepository<BookAuthorship> bookAuthorshipRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _bookAuthorshipRepository = bookAuthorshipRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportBookAuthorships request, CancellationToken cancellationToken)
        {
            IQueryable<BookAuthorship> query = _bookAuthorshipRepository.GetEntities()
                .Include(e => e.Book)
                .Include(e => e.Author);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var bookAuthorships = await query.ToListAsync(cancellationToken);
            var models = _mapper.Map<List<BookAuthorship>, List<BookAuthorshipModel>>(bookAuthorships);
            return await _excelFormatter.GetExcelFileAsync(models, "BookAuthorships");
        }

    }
}
