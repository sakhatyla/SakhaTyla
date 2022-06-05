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
using SakhaTyla.Core.Requests.Comments.Models;

namespace SakhaTyla.Core.Requests.Comments
{
    public class ExportCommentsHandler : IRequestHandler<ExportComments, FileContentModel>
    {
        private readonly IEntityRepository<Comment> _commentRepository;
        private readonly IExcelFormatter _excelFormatter;
        private readonly IMapper _mapper;

        public ExportCommentsHandler(IEntityRepository<Comment> commentRepository,
            IExcelFormatter excelFormatter,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _excelFormatter = excelFormatter;
            _mapper = mapper;
        }

        public async Task<FileContentModel> Handle(ExportComments request, CancellationToken cancellationToken)
        {
            IQueryable<Comment> query = _commentRepository.GetEntities()
                .Include(e => e.Container)
                .Include(e => e.Author)
                .Include(e => e.Parent);            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var comments = await query.ToListAsync(cancellationToken);
            var models = _mapper.Map<List<Comment>, List<CommentModel>>(comments);
            return await _excelFormatter.GetExcelFileAsync(models, "Comments");
        }

    }
}
