using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Widgets.Models;

namespace SakhaTyla.Core.Requests.Widgets
{
    public class GetWidgetsHandler : IRequestHandler<GetWidgets, PageModel<WidgetModel>>
    {
        private readonly IEntityRepository<Widget> _widgetRepository;
        private readonly IMapper _mapper;

        public GetWidgetsHandler(IEntityRepository<Widget> widgetRepository,
            IMapper mapper)
        {
            _widgetRepository = widgetRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<WidgetModel>> Handle(GetWidgets request, CancellationToken cancellationToken)
        {
            IQueryable<Widget> query = _widgetRepository.GetEntities();            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var widgets = await query.ToPagedListAsync(request.PageIndex, request.PageSize, cancellationToken);
            return widgets.Map<Widget, WidgetModel>(_mapper);
        }

    }
}
