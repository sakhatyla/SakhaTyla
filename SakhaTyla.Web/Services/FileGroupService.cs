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
using SakhaTyla.Core.Requests.FileGroups;
using SakhaTyla.Core.Requests.FileGroups.Models;
using SakhaTyla.Web.Protos;
using SakhaTyla.Web.Protos.FileGroups;

namespace SakhaTyla.Web.Services
{
    [Authorize("ReadFileGroup")]
    public class FileGroupService : Protos.FileGroups.FileGroupService.FileGroupServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public FileGroupService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<FileGroupPageModel> GetFileGroups(GetFileGroupsRequest getFileGroupsRequest, ServerCallContext context)
        {
            var getFileGroups = _mapper.Map<GetFileGroupsRequest, GetFileGroups>(getFileGroupsRequest);
            return _mapper.Map<PageModel<FileGroupModel>, FileGroupPageModel>(await _mediator.Send(getFileGroups));
        }

        public override async Task<FileGroup> GetFileGroup(GetFileGroupRequest getFileGroupRequest, ServerCallContext context)
        {
            var getFileGroup = _mapper.Map<GetFileGroupRequest, GetFileGroup>(getFileGroupRequest);
            return _mapper.Map<FileGroupModel, FileGroup>(await _mediator.Send(getFileGroup));
        }

        [Authorize("WriteFileGroup")]
        public override async Task<Empty> UpdateFileGroup(UpdateFileGroupRequest updateFileGroupRequest, ServerCallContext context)
        {
            var updateFileGroup = _mapper.Map<UpdateFileGroupRequest, UpdateFileGroup>(updateFileGroupRequest);
            return _mapper.Map<Unit, Empty>(await _mediator.Send(updateFileGroup));
        }

        [Authorize("WriteFileGroup")]
        public override async Task<CreatedEntity> CreateFileGroup(CreateFileGroupRequest createFileGroupRequest, ServerCallContext context)
        {
            var createFileGroup = _mapper.Map<CreateFileGroupRequest, CreateFileGroup>(createFileGroupRequest);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(await _mediator.Send(createFileGroup));
        }

        [Authorize("WriteFileGroup")]
        public override async Task<Empty> DeleteFileGroup(DeleteFileGroupRequest deleteFileGroupRequest, ServerCallContext context)
        {
            var deleteFileGroup = _mapper.Map<DeleteFileGroupRequest, DeleteFileGroup>(deleteFileGroupRequest);
            return _mapper.Map<Unit, Empty>(await _mediator.Send(deleteFileGroup));
        }
    }
}
