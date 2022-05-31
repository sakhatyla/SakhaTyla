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
using SakhaTyla.Core.Requests.Widgets;
using SakhaTyla.Core.Requests.Widgets.Models;
using SakhaTyla.Web.Protos;
using SakhaTyla.Web.Protos.Widgets;

namespace SakhaTyla.Web.Services
{
    [Authorize("ReadWidget")]
    public class WidgetService : Protos.Widgets.WidgetService.WidgetServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public WidgetService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<WidgetPageModel> GetWidgets(GetWidgetsRequest getWidgetsRequest, ServerCallContext context)
        {
            var getWidgets = _mapper.Map<GetWidgetsRequest, GetWidgets>(getWidgetsRequest);
            var model = await _mediator.Send(getWidgets);
            return _mapper.Map<PageModel<WidgetModel>, WidgetPageModel>(model);
        }

        public override async Task<Widget> GetWidget(GetWidgetRequest getWidgetRequest, ServerCallContext context)
        {
            var getWidget = _mapper.Map<GetWidgetRequest, GetWidget>(getWidgetRequest);
            var model = await _mediator.Send(getWidget);
            return _mapper.Map<WidgetModel, Widget>(model!);
        }

        [Authorize("WriteWidget")]
        public override async Task<Empty> UpdateWidget(UpdateWidgetRequest updateWidgetRequest, ServerCallContext context)
        {
            var updateWidget = _mapper.Map<UpdateWidgetRequest, UpdateWidget>(updateWidgetRequest);
            var model = await _mediator.Send(updateWidget);
            return _mapper.Map<Unit, Empty>(model);
        }

        [Authorize("WriteWidget")]
        public override async Task<CreatedEntity> CreateWidget(CreateWidgetRequest createWidgetRequest, ServerCallContext context)
        {
            var createWidget = _mapper.Map<CreateWidgetRequest, CreateWidget>(createWidgetRequest);
            var model = await _mediator.Send(createWidget);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(model);
        }

        [Authorize("WriteWidget")]
        public override async Task<Empty> DeleteWidget(DeleteWidgetRequest deleteWidgetRequest, ServerCallContext context)
        {
            var deleteWidget = _mapper.Map<DeleteWidgetRequest, DeleteWidget>(deleteWidgetRequest);
            var model = await _mediator.Send(deleteWidget);
            return _mapper.Map<Unit, Empty>(model);
        }
    }
}
