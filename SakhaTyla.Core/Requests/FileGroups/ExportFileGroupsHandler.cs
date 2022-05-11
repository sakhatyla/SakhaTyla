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
using SakhaTyla.Core.Requests.FileGroups.Models;

namespace SakhaTyla.Core.Requests.FileGroups
{
    public class ExportFileGroupsHandler : IRequestHandler<ExportFileGroups, FileContentModel>
    {
        private readonly IEntityRepository<FileGroup> _fileGroupRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportFileGroupsHandler(IEntityRepository<FileGroup> fileGroupRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _fileGroupRepository = fileGroupRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportFileGroups request, CancellationToken cancellationToken)
        {
            IQueryable<FileGroup> query = _fileGroupRepository.GetEntities();            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var fileGroups = await query.ToListAsync(cancellationToken);
            var models = _mapper.Map<List<FileGroup>, List<FileGroupModel>>(fileGroups);
            return await _excelFormatter.GetExcelFileAsync(models, "FileGroups");
        }

    }
}
