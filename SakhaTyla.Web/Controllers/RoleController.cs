using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Roles;
using SakhaTyla.Core.Requests.Roles.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadRole")]
    [ValidateModel]
    [Route("api")]
    public class RoleController : Controller
    {
        private readonly IMediator _mediator;

        public RoleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetRoles")]
        public async Task<PageModel<RoleModel>> GetRolesAsync([FromBody] GetRoles getRoles)
        {
            return await _mediator.Send(getRoles);
        }

        [HttpPost("GetRole")]
        public async Task<RoleModel?> GetRoleAsync([FromBody] GetRole getRole)
        {
            return await _mediator.Send(getRole);
        }

        [HttpPost("ExportRoles")]
        public async Task<FileResult> ExportRolesAsync([FromBody] ExportRoles exportRoles)
        {
            var file = await _mediator.Send(exportRoles);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteRole")]
        [HttpPost("UpdateRole")]
        public async Task<Unit> UpdateRoleAsync([FromBody] UpdateRole updateRole)
        {
            return await _mediator.Send(updateRole);
        }

        [Authorize("WriteRole")]
        [HttpPost("CreateRole")]
        public async Task<CreatedEntity<int>> CreateRoleAsync([FromBody] CreateRole createRole)
        {
            return await _mediator.Send(createRole);
        }

        [Authorize("WriteRole")]
        [HttpPost("DeleteRole")]
        public async Task<Unit> DeleteRoleAsync([FromBody] DeleteRole deleteRole)
        {
            return await _mediator.Send(deleteRole);
        }
    }
}
