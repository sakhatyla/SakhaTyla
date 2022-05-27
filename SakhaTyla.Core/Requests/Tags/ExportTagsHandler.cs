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
using SakhaTyla.Core.Requests.Tags.Models;

namespace SakhaTyla.Core.Requests.Tags
{
    public class ExportTagsHandler : IRequestHandler<ExportTags, FileContentModel>
    {
        private readonly IEntityRepository<Tag> _tagRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportTagsHandler(IEntityRepository<Tag> tagRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _tagRepository = tagRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportTags request, CancellationToken cancellationToken)
        {
            IQueryable<Tag> query = _tagRepository.GetEntities();            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var tags = await query.ToListAsync(cancellationToken);
            var models = _mapper.Map<List<Tag>, List<TagModel>>(tags);
            return await _excelFormatter.GetExcelFileAsync(models, "Tags");
        }

    }
}
