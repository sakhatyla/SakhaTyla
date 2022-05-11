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
using SakhaTyla.Core.Requests.Files.Models;

namespace SakhaTyla.Core.Requests.Files
{
    public class ExportFilesHandler : IRequestHandler<ExportFiles, FileContentModel>
    {
        private readonly IEntityRepository<File> _fileRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportFilesHandler(IEntityRepository<File> fileRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _fileRepository = fileRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportFiles request, CancellationToken cancellationToken)
        {
            IQueryable<File> query = _fileRepository.GetEntities()
                .Include(e => e.Group);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var files = await query.ToListAsync(cancellationToken);
            var models = _mapper.Map<List<File>, List<FileModel>>(files);
            return await _excelFormatter.GetExcelFileAsync(models, "Files");
        }

    }
}
