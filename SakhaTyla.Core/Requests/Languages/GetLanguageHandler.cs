using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Languages.Models;

namespace SakhaTyla.Core.Requests.Languages
{
    public class GetLanguageHandler : IRequestHandler<GetLanguage, LanguageModel?>
    {
        private readonly IEntityRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public GetLanguageHandler(IEntityRepository<Language> languageRepository,
            IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<LanguageModel?> Handle(GetLanguage request, CancellationToken cancellationToken)
        {
            var language = await _languageRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            if (language == null)
            {
                return null;
            }
            return _mapper.Map<Language, LanguageModel>(language);
        }

    }
}
