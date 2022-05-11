using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.FileGroups;
using SakhaTyla.Core.Requests.FileGroups.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadFileGroup")]
    [ValidateModel]
    [Route("api")]
    public class FileGroupController : Controller
    {
        private readonly IMediator _mediator;

        public FileGroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetFileGroups")]
        public async Task<PageModel<FileGroupModel>> GetFileGroupsAsync([FromBody] GetFileGroups getFileGroups)
        {
            return await _mediator.Send(getFileGroups);
        }

        [HttpPost("GetFileGroup")]
        public async Task<FileGroupModel?> GetFileGroupAsync([FromBody] GetFileGroup getFileGroup)
        {
            return await _mediator.Send(getFileGroup);
        }

        [HttpPost("ExportFileGroups")]
        public async Task<FileResult> ExportFileGroupsAsync([FromBody] ExportFileGroups exportFileGroups)
        {
            var file = await _mediator.Send(exportFileGroups);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteFileGroup")]
        [HttpPost("UpdateFileGroup")]
        public async Task<Unit> UpdateFileGroupAsync([FromBody] UpdateFileGroup updateFileGroup)
        {
            return await _mediator.Send(updateFileGroup);
        }

        [Authorize("WriteFileGroup")]
        [HttpPost("CreateFileGroup")]
        public async Task<CreatedEntity<int>> CreateFileGroupAsync([FromBody] CreateFileGroup createFileGroup)
        {
            return await _mediator.Send(createFileGroup);
        }

        [Authorize("WriteFileGroup")]
        [HttpPost("DeleteFileGroup")]
        public async Task<Unit> DeleteFileGroupAsync([FromBody] DeleteFileGroup deleteFileGroup)
        {
            return await _mediator.Send(deleteFileGroup);
        }
    }
}
