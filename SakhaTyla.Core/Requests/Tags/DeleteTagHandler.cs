using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.Tags
{
    public class DeleteTagHandler : IRequestHandler<DeleteTag>
    {
        private readonly IEntityRepository<Tag> _tagRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteTagHandler(IEntityRepository<Tag> tagRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> localizer)
        {
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeleteTag request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (tag == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Tag"], request.Id]);
            }
            _tagRepository.Delete(tag);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
