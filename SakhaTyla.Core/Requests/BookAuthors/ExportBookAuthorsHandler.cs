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
using SakhaTyla.Core.Requests.BookAuthors.Models;

namespace SakhaTyla.Core.Requests.BookAuthors
{
    public class ExportBookAuthorsHandler : IRequestHandler<ExportBookAuthors, FileContentModel>
    {
        private readonly IEntityRepository<BookAuthor> _bookAuthorRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportBookAuthorsHandler(IEntityRepository<BookAuthor> bookAuthorRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _bookAuthorRepository = bookAuthorRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportBookAuthors request, CancellationToken cancellationToken)
        {
            IQueryable<BookAuthor> query = _bookAuthorRepository.GetEntities();            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var bookAuthors = await query.ToListAsync(cancellationToken);
            var models = _mapper.Map<List<BookAuthor>, List<BookAuthorModel>>(bookAuthors);
            return await _excelFormatter.GetExcelFileAsync(models, "BookAuthors");
        }

    }
}
