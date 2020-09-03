using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Languages.Models;

namespace SakhaTyla.Core.Requests.Languages
{
    public class GetLanguagesHandler : IRequestHandler<GetLanguages, PageModel<LanguageModel>>
    {
        private readonly IEntityRepository<Language> _languageRepository;
        private readonly IMapper _mapper;

        public GetLanguagesHandler(IEntityRepository<Language> languageRepository,
            IMapper mapper)
        {
            _languageRepository = languageRepository;
            _mapper = mapper;
        }

        public async Task<PageModel<LanguageModel>> Handle(GetLanguages request, CancellationToken cancellationToken)
        {
            IQueryable<Language> query = _languageRepository.GetEntities();            
            query = query.Filter(request.Filter);
            query = query.OrderBy(request.OrderBy, request.OrderDirection);
            var languages = await query.ToPagedListAsync(request.PageIndex, request.PageSize);
            return languages.Map<Language, LanguageModel>(_mapper);
        }

    }
}
