using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Files;
using SakhaTyla.Core.Requests.Files.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadFile")]
    [ValidateModel]
    [Route("api")]
    public class FileController : Controller
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetFiles")]
        public async Task<PageModel<FileModel>> GetFilesAsync([FromBody] GetFiles getFiles)
        {
            return await _mediator.Send(getFiles);
        }

        [HttpPost("GetFile")]
        public async Task<FileModel?> GetFileAsync([FromBody] GetFile getFile)
        {
            return await _mediator.Send(getFile);
        }

        [HttpPost("DownloadFile")]
        public async Task<FileResult> DownloadFileAsync([FromBody] DownloadFile downloadFile)
        {
            var file = await _mediator.Send(downloadFile);
            return File(file.Content, file.ContentType, file.Name);
        }

        [HttpPost("ExportFiles")]
        public async Task<FileResult> ExportFilesAsync([FromBody] ExportFiles exportFiles)
        {
            var file = await _mediator.Send(exportFiles);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteFile")]
        [HttpPost("UpdateFile")]
        public async Task<Unit> UpdateFileAsync(int id, IFormFile file)
        {
            return await _mediator.Send(new UpdateFile()
            {
                Id = id,
                Name = file.FileName,
                ContentType = file.ContentType,
                Content = file.OpenReadStream(),
            });
        }

        [Authorize("WriteFile")]
        [HttpPost("CreateFile")]
        public async Task<CreatedEntity<int>> CreateFileAsync(int groupId, IFormFile file)
        {
            return await _mediator.Send(new CreateFile()
            {
                Name = file.FileName,
                ContentType = file.ContentType,
                Content = file.OpenReadStream(),
                GroupId = groupId,
            });
        }

        [Authorize("WriteFile")]
        [HttpPost("DeleteFile")]
        public async Task<Unit> DeleteFileAsync([FromBody] DeleteFile deleteFile)
        {
            return await _mediator.Send(deleteFile);
        }
    }
}
