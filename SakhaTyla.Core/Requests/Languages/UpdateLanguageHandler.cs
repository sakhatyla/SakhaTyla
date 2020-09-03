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

namespace SakhaTyla.Core.Requests.Languages
{
    public class UpdateLanguageHandler : IRequestHandler<UpdateLanguage>
    {
        private readonly IEntityRepository<Language> _languageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateLanguageHandler(IEntityRepository<Language> languageRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _languageRepository = languageRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(UpdateLanguage request, CancellationToken cancellationToken)
        {
            var language = await _languageRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync();
            if (language == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Language"], request.Id]);
            }
            _mapper.Map(request, language);
            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }

    }
}
