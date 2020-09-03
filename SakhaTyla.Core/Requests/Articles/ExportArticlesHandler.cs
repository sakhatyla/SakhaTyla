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
using SakhaTyla.Core.Requests.Articles.Models;

namespace SakhaTyla.Core.Requests.Articles
{
    public class ExportArticlesHandler : IRequestHandler<ExportArticles, FileContentModel>
    {
        private readonly IEntityRepository<Article> _articleRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportArticlesHandler(IEntityRepository<Article> articleRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _articleRepository = articleRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportArticles request, CancellationToken cancellationToken)
        {
            IQueryable<Article> query = _articleRepository.GetEntities()
                .Include(e => e.FromLanguage)
                .Include(e => e.ToLanguage)
                .Include(e => e.Category);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var articles = await query.ToListAsync();
            var models = _mapper.Map<List<Article>, List<ArticleModel>>(articles);
            return await _excelFormatter.GetExcelFileAsync(models, "Articles");
        }

    }
}
