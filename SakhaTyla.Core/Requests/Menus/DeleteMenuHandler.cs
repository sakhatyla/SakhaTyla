using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.Menus
{
    public class DeleteMenuHandler : IRequestHandler<DeleteMenu>
    {
        private readonly IEntityRepository<Menu> _menuRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteMenuHandler(IEntityRepository<Menu> menuRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> localizer)
        {
            _menuRepository = menuRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeleteMenu request, CancellationToken cancellationToken)
        {
            var menu = await _menuRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (menu == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Menu"], request.Id]);
            }
            _menuRepository.Delete(menu);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
