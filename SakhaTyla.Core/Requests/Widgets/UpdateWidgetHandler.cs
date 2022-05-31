using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.Widgets
{
    public class UpdateWidgetHandler : IRequestHandler<UpdateWidget>
    {
        private readonly IEntityRepository<Widget> _widgetRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateWidgetHandler(IEntityRepository<Widget> widgetRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _widgetRepository = widgetRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateWidget request, CancellationToken cancellationToken)
        {
            var widget = await _widgetRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (widget == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Widget"], request.Id]);
            }
            _mapper.Map(request, widget);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
