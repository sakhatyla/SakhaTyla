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
using SakhaTyla.Core.Requests.WorkerScheduleTasks;
using SakhaTyla.Core.Requests.WorkerScheduleTasks.Models;
using SakhaTyla.Web.Protos;
using SakhaTyla.Web.Protos.WorkerScheduleTasks;

namespace SakhaTyla.Web.Services
{
    [Authorize("ReadWorkerScheduleTask")]
    public class WorkerScheduleTaskService : Protos.WorkerScheduleTasks.WorkerScheduleTaskService.WorkerScheduleTaskServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public WorkerScheduleTaskService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<WorkerScheduleTaskPageModel> GetWorkerScheduleTasks(GetWorkerScheduleTasksRequest getWorkerScheduleTasksRequest, ServerCallContext context)
        {
            var getWorkerScheduleTasks = _mapper.Map<GetWorkerScheduleTasksRequest, GetWorkerScheduleTasks>(getWorkerScheduleTasksRequest);
            var model = await _mediator.Send(getWorkerScheduleTasks);
            return _mapper.Map<PageModel<WorkerScheduleTaskModel>, WorkerScheduleTaskPageModel>(model);
        }

        public override async Task<WorkerScheduleTask> GetWorkerScheduleTask(GetWorkerScheduleTaskRequest getWorkerScheduleTaskRequest, ServerCallContext context)
        {
            var getWorkerScheduleTask = _mapper.Map<GetWorkerScheduleTaskRequest, GetWorkerScheduleTask>(getWorkerScheduleTaskRequest);
            var model = await _mediator.Send(getWorkerScheduleTask);
            return _mapper.Map<WorkerScheduleTaskModel, WorkerScheduleTask>(model!);
        }

        [Authorize("WriteWorkerScheduleTask")]
        public override async Task<Empty> UpdateWorkerScheduleTask(UpdateWorkerScheduleTaskRequest updateWorkerScheduleTaskRequest, ServerCallContext context)
        {
            var updateWorkerScheduleTask = _mapper.Map<UpdateWorkerScheduleTaskRequest, UpdateWorkerScheduleTask>(updateWorkerScheduleTaskRequest);
            var model = await _mediator.Send(updateWorkerScheduleTask);
            return _mapper.Map<Unit, Empty>(model);
        }

        [Authorize("WriteWorkerScheduleTask")]
        public override async Task<CreatedEntity> CreateWorkerScheduleTask(CreateWorkerScheduleTaskRequest createWorkerScheduleTaskRequest, ServerCallContext context)
        {
            var createWorkerScheduleTask = _mapper.Map<CreateWorkerScheduleTaskRequest, CreateWorkerScheduleTask>(createWorkerScheduleTaskRequest);
            var model = await _mediator.Send(createWorkerScheduleTask);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(model);
        }

        [Authorize("WriteWorkerScheduleTask")]
        public override async Task<Empty> DeleteWorkerScheduleTask(DeleteWorkerScheduleTaskRequest deleteWorkerScheduleTaskRequest, ServerCallContext context)
        {
            var deleteWorkerScheduleTask = _mapper.Map<DeleteWorkerScheduleTaskRequest, DeleteWorkerScheduleTask>(deleteWorkerScheduleTaskRequest);
            var model = await _mediator.Send(deleteWorkerScheduleTask);
            return _mapper.Map<Unit, Empty>(model);
        }
    }
}
