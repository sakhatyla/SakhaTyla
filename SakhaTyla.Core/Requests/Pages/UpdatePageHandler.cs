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

namespace SakhaTyla.Core.Requests.Pages
{
    public class UpdatePageHandler : IRequestHandler<UpdatePage>
    {
        private readonly IEntityRepository<Page> _pageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdatePageHandler(IEntityRepository<Page> pageRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _pageRepository = pageRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdatePage request, CancellationToken cancellationToken)
        {
            var page = await _pageRepository.GetEntities()
                .Include(e => e.Route)
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (page == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Page"], request.Id]);
            }
            _mapper.Map(request, page);
            await _pageRepository.CheckTreeLoop(page);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _pageRepository.CalculateTree(page);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
