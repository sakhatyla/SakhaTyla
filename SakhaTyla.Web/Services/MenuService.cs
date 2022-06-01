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
using SakhaTyla.Core.Requests.Menus;
using SakhaTyla.Core.Requests.Menus.Models;
using SakhaTyla.Web.Protos;
using SakhaTyla.Web.Protos.Menus;

namespace SakhaTyla.Web.Services
{
    [Authorize("ReadMenu")]
    public class MenuService : Protos.Menus.MenuService.MenuServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MenuService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<MenuPageModel> GetMenus(GetMenusRequest getMenusRequest, ServerCallContext context)
        {
            var getMenus = _mapper.Map<GetMenusRequest, GetMenus>(getMenusRequest);
            var model = await _mediator.Send(getMenus);
            return _mapper.Map<PageModel<MenuModel>, MenuPageModel>(model);
        }

        public override async Task<Menu> GetMenu(GetMenuRequest getMenuRequest, ServerCallContext context)
        {
            var getMenu = _mapper.Map<GetMenuRequest, GetMenu>(getMenuRequest);
            var model = await _mediator.Send(getMenu);
            return _mapper.Map<MenuModel, Menu>(model!);
        }

        [Authorize("WriteMenu")]
        public override async Task<Empty> UpdateMenu(UpdateMenuRequest updateMenuRequest, ServerCallContext context)
        {
            var updateMenu = _mapper.Map<UpdateMenuRequest, UpdateMenu>(updateMenuRequest);
            var model = await _mediator.Send(updateMenu);
            return _mapper.Map<Unit, Empty>(model);
        }

        [Authorize("WriteMenu")]
        public override async Task<CreatedEntity> CreateMenu(CreateMenuRequest createMenuRequest, ServerCallContext context)
        {
            var createMenu = _mapper.Map<CreateMenuRequest, CreateMenu>(createMenuRequest);
            var model = await _mediator.Send(createMenu);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(model);
        }

        [Authorize("WriteMenu")]
        public override async Task<Empty> DeleteMenu(DeleteMenuRequest deleteMenuRequest, ServerCallContext context)
        {
            var deleteMenu = _mapper.Map<DeleteMenuRequest, DeleteMenu>(deleteMenuRequest);
            var model = await _mediator.Send(deleteMenu);
            return _mapper.Map<Unit, Empty>(model);
        }
    }
}
