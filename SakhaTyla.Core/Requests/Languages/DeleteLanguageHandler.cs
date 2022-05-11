using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;

namespace SakhaTyla.Core.Requests.Languages
{
    public class DeleteLanguageHandler : IRequestHandler<DeleteLanguage>
    {
        private readonly IEntityRepository<Language> _languageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteLanguageHandler(IEntityRepository<Language> languageRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> localizer)
        {
            _languageRepository = languageRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeleteLanguage request, CancellationToken cancellationToken)
        {
            var language = await _languageRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (language == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Language"], request.Id]);
            }
            _languageRepository.Delete(language);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }

    }
}
