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
using SakhaTyla.Core.Requests.WorkerRuns.Models;

namespace SakhaTyla.Core.Requests.WorkerRuns
{
    public class ExportWorkerRunsHandler : IRequestHandler<ExportWorkerRuns, FileContentModel>
    {
        private readonly IEntityRepository<WorkerRun> _workerRunRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportWorkerRunsHandler(IEntityRepository<WorkerRun> workerRunRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _workerRunRepository = workerRunRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportWorkerRuns request, CancellationToken cancellationToken)
        {
            IQueryable<WorkerRun> query = _workerRunRepository.GetEntities()
                .Include(e => e.WorkerInfo);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var workerRuns = await query.ToListAsync(cancellationToken);
            var models = _mapper.Map<List<WorkerRun>, List<WorkerRunModel>>(workerRuns);
            return await _excelFormatter.GetExcelFileAsync(models, "WorkerRuns");
        }

    }
}
