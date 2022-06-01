using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Menus;
using SakhaTyla.Core.Requests.Menus.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadMenu")]
    [ValidateModel]
    [Route("api")]
    public class MenuController : Controller
    {
        private readonly IMediator _mediator;

        public MenuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetMenus")]
        public async Task<PageModel<MenuModel>> GetMenusAsync([FromBody] GetMenus getMenus)
        {
            return await _mediator.Send(getMenus);
        }

        [HttpPost("GetMenu")]
        public async Task<MenuModel?> GetMenuAsync([FromBody] GetMenu getMenu)
        {
            return await _mediator.Send(getMenu);
        }

        [HttpPost("ExportMenus")]
        public async Task<FileResult> ExportMenusAsync([FromBody] ExportMenus exportMenus)
        {
            var file = await _mediator.Send(exportMenus);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteMenu")]
        [HttpPost("UpdateMenu")]
        public async Task<Unit> UpdateMenuAsync([FromBody] UpdateMenu updateMenu)
        {
            return await _mediator.Send(updateMenu);
        }

        [Authorize("WriteMenu")]
        [HttpPost("CreateMenu")]
        public async Task<CreatedEntity<int>> CreateMenuAsync([FromBody] CreateMenu createMenu)
        {
            return await _mediator.Send(createMenu);
        }

        [Authorize("WriteMenu")]
        [HttpPost("DeleteMenu")]
        public async Task<Unit> DeleteMenuAsync([FromBody] DeleteMenu deleteMenu)
        {
            return await _mediator.Send(deleteMenu);
        }
    }
}