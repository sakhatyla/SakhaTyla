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
using SakhaTyla.Core.Requests.Files;
using SakhaTyla.Core.Requests.Files.Models;
using SakhaTyla.Web.Protos;
using SakhaTyla.Web.Protos.Files;

namespace SakhaTyla.Web.Services
{
    [Authorize("ReadFile")]
    public class FileService : Protos.Files.FileService.FileServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public FileService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<FilePageModel> GetFiles(GetFilesRequest getFilesRequest, ServerCallContext context)
        {
            var getFiles = _mapper.Map<GetFilesRequest, GetFiles>(getFilesRequest);
            var model = await _mediator.Send(getFiles);
            return _mapper.Map<PageModel<FileModel>, FilePageModel>(model);
        }

        public override async Task<File> GetFile(GetFileRequest getFileRequest, ServerCallContext context)
        {
            var getFile = _mapper.Map<GetFileRequest, GetFile>(getFileRequest);
            var model = await _mediator.Send(getFile);
            return _mapper.Map<FileModel, File>(model!);
        }

        [Authorize("WriteFile")]
        public override async Task<Empty> UpdateFile(UpdateFileRequest updateFileRequest, ServerCallContext context)
        {
            var updateFile = _mapper.Map<UpdateFileRequest, UpdateFile>(updateFileRequest);
            var model = await _mediator.Send(updateFile);
            return _mapper.Map<Unit, Empty>(model);
        }

        [Authorize("WriteFile")]
        public override async Task<CreatedEntity> CreateFile(CreateFileRequest createFileRequest, ServerCallContext context)
        {
            var createFile = _mapper.Map<CreateFileRequest, CreateFile>(createFileRequest);
            var model = await _mediator.Send(createFile);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(model);
        }

        [Authorize("WriteFile")]
        public override async Task<Empty> DeleteFile(DeleteFileRequest deleteFileRequest, ServerCallContext context)
        {
            var deleteFile = _mapper.Map<DeleteFileRequest, DeleteFile>(deleteFileRequest);
            var model = await _mediator.Send(deleteFile);
            return _mapper.Map<Unit, Empty>(model);
        }
    }
}
