using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Languages;
using SakhaTyla.Core.Requests.Languages.Models;
using SakhaTyla.Web.Protos;
using SakhaTyla.Web.Protos.Languages;

namespace SakhaTyla.Web.Services
{
    [Authorize("ReadLanguage")]
    public class LanguageService : Protos.Languages.LanguageService.LanguageServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LanguageService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<LanguagePageModel> GetLanguages(GetLanguagesRequest getLanguagesRequest, ServerCallContext context)
        {
            var getLanguages = _mapper.Map<GetLanguagesRequest, GetLanguages>(getLanguagesRequest);
            return _mapper.Map<PageModel<LanguageModel>, LanguagePageModel>(await _mediator.Send(getLanguages));
        }

        public override async Task<Language> GetLanguage(GetLanguageRequest getLanguageRequest, ServerCallContext context)
        {
            var getLanguage = _mapper.Map<GetLanguageRequest, GetLanguage>(getLanguageRequest);
            return _mapper.Map<LanguageModel, Language>(await _mediator.Send(getLanguage));
        }

        [Authorize("WriteLanguage")]
        public override async Task<Empty> UpdateLanguage(UpdateLanguageRequest updateLanguageRequest, ServerCallContext context)
        {
            var updateLanguage = _mapper.Map<UpdateLanguageRequest, UpdateLanguage>(updateLanguageRequest);
            return _mapper.Map<Unit, Empty>(await _mediator.Send(updateLanguage));
        }

        [Authorize("WriteLanguage")]
        public override async Task<CreatedEntity> CreateLanguage(CreateLanguageRequest createLanguageRequest, ServerCallContext context)
        {
            var createLanguage = _mapper.Map<CreateLanguageRequest, CreateLanguage>(createLanguageRequest);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(await _mediator.Send(createLanguage));
        }

        [Authorize("WriteLanguage")]
        public override async Task<Empty> DeleteLanguage(DeleteLanguageRequest deleteLanguageRequest, ServerCallContext context)
        {
            var deleteLanguage = _mapper.Map<DeleteLanguageRequest, DeleteLanguage>(deleteLanguageRequest);
            return _mapper.Map<Unit, Empty>(await _mediator.Send(deleteLanguage));
        }
    }
}
