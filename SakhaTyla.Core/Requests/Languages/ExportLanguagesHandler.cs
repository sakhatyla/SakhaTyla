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
using SakhaTyla.Core.Requests.Languages.Models;

namespace SakhaTyla.Core.Requests.Languages
{
    public class ExportLanguagesHandler : IRequestHandler<ExportLanguages, FileContentModel>
    {
        private readonly IEntityRepository<Language> _languageRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportLanguagesHandler(IEntityRepository<Language> languageRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _languageRepository = languageRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportLanguages request, CancellationToken cancellationToken)
        {
            IQueryable<Language> query = _languageRepository.GetEntities();            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var languages = await query.ToListAsync(cancellationToken);
            var models = _mapper.Map<List<Language>, List<LanguageModel>>(languages);
            return await _excelFormatter.GetExcelFileAsync(models, "Languages");
        }

    }
}
