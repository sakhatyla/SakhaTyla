using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Core.Requests.Languages
{
    public class CreateLanguageHandler : IRequestHandler<CreateLanguage, CreatedEntity<int>>
    {
        private readonly IEntityRepository<Language> _languageRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLanguageHandler(IEntityRepository<Language> languageRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _languageRepository = languageRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreatedEntity<int>> Handle(CreateLanguage request, CancellationToken cancellationToken)
        {
            var language = _mapper.Map<CreateLanguage, Language>(request);
            _languageRepository.Add(language);
            await _unitOfWork.CommitAsync();
            return new CreatedEntity<int>() { Id = language.Id };
        }

    }
}
