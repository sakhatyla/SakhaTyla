using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Widgets
{
    public class CreateWidgetHandler : IRequestHandler<CreateWidget, CreatedEntity<int>>
    {
        private readonly IEntityRepository<Widget> _widgetRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateWidgetHandler(IEntityRepository<Widget> widgetRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _widgetRepository = widgetRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreateWidget request, CancellationToken cancellationToken)
        {
            var widget = _mapper.Map<CreateWidget, Widget>(request);
            _widgetRepository.Add(widget);
            await _unitOfWork.CommitAsync(cancellationToken);
            return new CreatedEntity<int>(widget.Id);
        }

    }
}
