using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.WorkerScheduleTasks;
using SakhaTyla.Core.Requests.WorkerScheduleTasks.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadWorkerScheduleTask")]
    [ValidateModel]
    [Route("api")]
    public class WorkerScheduleTaskController : Controller
    {
        private readonly IMediator _mediator;

        public WorkerScheduleTaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetWorkerScheduleTasks")]
        public async Task<PageModel<WorkerScheduleTaskModel>> GetWorkerScheduleTasksAsync([FromBody] GetWorkerScheduleTasks getWorkerScheduleTasks)
        {
            return await _mediator.Send(getWorkerScheduleTasks);
        }

        [HttpPost("GetWorkerScheduleTask")]
        public async Task<WorkerScheduleTaskModel?> GetWorkerScheduleTaskAsync([FromBody] GetWorkerScheduleTask getWorkerScheduleTask)
        {
            return await _mediator.Send(getWorkerScheduleTask);
        }

        [HttpPost("ExportWorkerScheduleTasks")]
        public async Task<FileResult> ExportWorkerScheduleTasksAsync([FromBody] ExportWorkerScheduleTasks exportWorkerScheduleTasks)
        {
            var file = await _mediator.Send(exportWorkerScheduleTasks);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteWorkerScheduleTask")]
        [HttpPost("UpdateWorkerScheduleTask")]
        public async Task<Unit> UpdateWorkerScheduleTaskAsync([FromBody] UpdateWorkerScheduleTask updateWorkerScheduleTask)
        {
            return await _mediator.Send(updateWorkerScheduleTask);
        }

        [Authorize("WriteWorkerScheduleTask")]
        [HttpPost("CreateWorkerScheduleTask")]
        public async Task<CreatedEntity<int>> CreateWorkerScheduleTaskAsync([FromBody] CreateWorkerScheduleTask createWorkerScheduleTask)
        {
            return await _mediator.Send(createWorkerScheduleTask);
        }

        [Authorize("WriteWorkerScheduleTask")]
        [HttpPost("DeleteWorkerScheduleTask")]
        public async Task<Unit> DeleteWorkerScheduleTaskAsync([FromBody] DeleteWorkerScheduleTask deleteWorkerScheduleTask)
        {
            return await _mediator.Send(deleteWorkerScheduleTask);
        }
    }
}