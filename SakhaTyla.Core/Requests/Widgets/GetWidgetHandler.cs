using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Widgets.Models;

namespace SakhaTyla.Core.Requests.Widgets
{
    public class GetWidgetHandler : IRequestHandler<GetWidget, WidgetModel?>
    {
        private readonly IEntityRepository<Widget> _widgetRepository;
        private readonly IMapper _mapper;

        public GetWidgetHandler(IEntityRepository<Widget> widgetRepository,
            IMapper mapper)
        {
            _widgetRepository = widgetRepository;
            _mapper = mapper;
        }

        public async Task<WidgetModel?> Handle(GetWidget request, CancellationToken cancellationToken)
        {
            IQueryable<Widget> query = _widgetRepository.GetEntities();
            if (request.Id != null)
            {
                query = query.Where(e => e.Id == request.Id);
            }
            else if (!string.IsNullOrEmpty(request.Code))
            {
                query = query.Where(e => e.Code == request.Code);
            }
            else
            {
                return null;
            }

            var widget = await query.FirstOrDefaultAsync();
            if (widget == null)
            {
                return null;
            }
            return _mapper.Map<Widget, WidgetModel>(widget);
        }

    }
}
