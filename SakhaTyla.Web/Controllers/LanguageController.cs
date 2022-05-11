using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Languages;
using SakhaTyla.Core.Requests.Languages.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadLanguage")]
    [ValidateModel]
    [Route("api")]
    public class LanguageController : Controller
    {
        private readonly IMediator _mediator;

        public LanguageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetLanguages")]
        public async Task<PageModel<LanguageModel>> GetLanguagesAsync([FromBody] GetLanguages getLanguages)
        {
            return await _mediator.Send(getLanguages);
        }

        [HttpPost("GetLanguage")]
        public async Task<LanguageModel?> GetLanguageAsync([FromBody] GetLanguage getLanguage)
        {
            return await _mediator.Send(getLanguage);
        }

        [HttpPost("ExportLanguages")]
        public async Task<FileResult> ExportLanguagesAsync([FromBody] ExportLanguages exportLanguages)
        {
            var file = await _mediator.Send(exportLanguages);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteLanguage")]
        [HttpPost("UpdateLanguage")]
        public async Task<Unit> UpdateLanguageAsync([FromBody] UpdateLanguage updateLanguage)
        {
            return await _mediator.Send(updateLanguage);
        }

        [Authorize("WriteLanguage")]
        [HttpPost("CreateLanguage")]
        public async Task<CreatedEntity<int>> CreateLanguageAsync([FromBody] CreateLanguage createLanguage)
        {
            return await _mediator.Send(createLanguage);
        }

        [Authorize("WriteLanguage")]
        [HttpPost("DeleteLanguage")]
        public async Task<Unit> DeleteLanguageAsync([FromBody] DeleteLanguage deleteLanguage)
        {
            return await _mediator.Send(deleteLanguage);
        }
    }
}