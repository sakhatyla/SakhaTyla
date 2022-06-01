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
using SakhaTyla.Core.Requests.MenuItems;
using SakhaTyla.Core.Requests.MenuItems.Models;
using SakhaTyla.Web.Protos;
using SakhaTyla.Web.Protos.MenuItems;

namespace SakhaTyla.Web.Services
{
    [Authorize("ReadMenuItem")]
    public class MenuItemService : Protos.MenuItems.MenuItemService.MenuItemServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MenuItemService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<MenuItemPageModel> GetMenuItems(GetMenuItemsRequest getMenuItemsRequest, ServerCallContext context)
        {
            var getMenuItems = _mapper.Map<GetMenuItemsRequest, GetMenuItems>(getMenuItemsRequest);
            var model = await _mediator.Send(getMenuItems);
            return _mapper.Map<PageModel<MenuItemModel>, MenuItemPageModel>(model);
        }

        public override async Task<MenuItem> GetMenuItem(GetMenuItemRequest getMenuItemRequest, ServerCallContext context)
        {
            var getMenuItem = _mapper.Map<GetMenuItemRequest, GetMenuItem>(getMenuItemRequest);
            var model = await _mediator.Send(getMenuItem);
            return _mapper.Map<MenuItemModel, MenuItem>(model!);
        }

        [Authorize("WriteMenuItem")]
        public override async Task<Empty> UpdateMenuItem(UpdateMenuItemRequest updateMenuItemRequest, ServerCallContext context)
        {
            var updateMenuItem = _mapper.Map<UpdateMenuItemRequest, UpdateMenuItem>(updateMenuItemRequest);
            var model = await _mediator.Send(updateMenuItem);
            return _mapper.Map<Unit, Empty>(model);
        }

        [Authorize("WriteMenuItem")]
        public override async Task<CreatedEntity> CreateMenuItem(CreateMenuItemRequest createMenuItemRequest, ServerCallContext context)
        {
            var createMenuItem = _mapper.Map<CreateMenuItemRequest, CreateMenuItem>(createMenuItemRequest);
            var model = await _mediator.Send(createMenuItem);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(model);
        }

        [Authorize("WriteMenuItem")]
        public override async Task<Empty> DeleteMenuItem(DeleteMenuItemRequest deleteMenuItemRequest, ServerCallContext context)
        {
            var deleteMenuItem = _mapper.Map<DeleteMenuItemRequest, DeleteMenuItem>(deleteMenuItemRequest);
            var model = await _mediator.Send(deleteMenuItem);
            return _mapper.Map<Unit, Empty>(model);
        }
    }
}
