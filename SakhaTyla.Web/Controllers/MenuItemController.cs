using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.MenuItems;
using SakhaTyla.Core.Requests.MenuItems.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadMenuItem")]
    [ValidateModel]
    [Route("api")]
    public class MenuItemController : Controller
    {
        private readonly IMediator _mediator;

        public MenuItemController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetMenuItems")]
        public async Task<PageModel<MenuItemModel>> GetMenuItemsAsync([FromBody] GetMenuItems getMenuItems)
        {
            return await _mediator.Send(getMenuItems);
        }

        [HttpPost("GetMenuItem")]
        public async Task<MenuItemModel?> GetMenuItemAsync([FromBody] GetMenuItem getMenuItem)
        {
            return await _mediator.Send(getMenuItem);
        }

        [HttpPost("ExportMenuItems")]
        public async Task<FileResult> ExportMenuItemsAsync([FromBody] ExportMenuItems exportMenuItems)
        {
            var file = await _mediator.Send(exportMenuItems);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteMenuItem")]
        [HttpPost("UpdateMenuItem")]
        public async Task<Unit> UpdateMenuItemAsync([FromBody] UpdateMenuItem updateMenuItem)
        {
            return await _mediator.Send(updateMenuItem);
        }

        [Authorize("WriteMenu")]
        [HttpPost("UpdateMenuItemWeight")]
        public async Task<Unit> UpdateMenuItemWeightAsync([FromBody] UpdateMenuItemWeight updateMenuItemWeight)
        {
            return await _mediator.Send(updateMenuItemWeight);
        }

        [Authorize("WriteMenuItem")]
        [HttpPost("CreateMenuItem")]
        public async Task<CreatedEntity<int>> CreateMenuItemAsync([FromBody] CreateMenuItem createMenuItem)
        {
            return await _mediator.Send(createMenuItem);
        }

        [Authorize("WriteMenuItem")]
        [HttpPost("DeleteMenuItem")]
        public async Task<Unit> DeleteMenuItemAsync([FromBody] DeleteMenuItem deleteMenuItem)
        {
            return await _mediator.Send(deleteMenuItem);
        }
    }
}
