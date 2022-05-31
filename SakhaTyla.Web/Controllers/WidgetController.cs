using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Widgets;
using SakhaTyla.Core.Requests.Widgets.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadWidget")]
    [ValidateModel]
    [Route("api")]
    public class WidgetController : Controller
    {
        private readonly IMediator _mediator;

        public WidgetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetWidgets")]
        public async Task<PageModel<WidgetModel>> GetWidgetsAsync([FromBody] GetWidgets getWidgets)
        {
            return await _mediator.Send(getWidgets);
        }

        [HttpPost("GetWidget")]
        public async Task<WidgetModel?> GetWidgetAsync([FromBody] GetWidget getWidget)
        {
            return await _mediator.Send(getWidget);
        }

        [HttpPost("ExportWidgets")]
        public async Task<FileResult> ExportWidgetsAsync([FromBody] ExportWidgets exportWidgets)
        {
            var file = await _mediator.Send(exportWidgets);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteWidget")]
        [HttpPost("UpdateWidget")]
        public async Task<Unit> UpdateWidgetAsync([FromBody] UpdateWidget updateWidget)
        {
            return await _mediator.Send(updateWidget);
        }

        [Authorize("WriteWidget")]
        [HttpPost("CreateWidget")]
        public async Task<CreatedEntity<int>> CreateWidgetAsync([FromBody] CreateWidget createWidget)
        {
            return await _mediator.Send(createWidget);
        }

        [Authorize("WriteWidget")]
        [HttpPost("DeleteWidget")]
        public async Task<Unit> DeleteWidgetAsync([FromBody] DeleteWidget deleteWidget)
        {
            return await _mediator.Send(deleteWidget);
        }
    }
}