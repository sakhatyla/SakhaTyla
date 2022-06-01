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
using SakhaTyla.Core.Requests.Books.Models;

namespace SakhaTyla.Core.Requests.Books
{
    public class ExportBooksHandler : IRequestHandler<ExportBooks, FileContentModel>
    {
        private readonly IEntityRepository<Book> _bookRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportBooksHandler(IEntityRepository<Book> bookRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportBooks request, CancellationToken cancellationToken)
        {
            IQueryable<Book> query = _bookRepository.GetEntities();            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var books = await query.ToListAsync(cancellationToken);
            var models = _mapper.Map<List<Book>, List<BookModel>>(books);
            return await _excelFormatter.GetExcelFileAsync(models, "Books");
        }

    }
}
