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
            return _mapper.Map<PageModel<FileModel>, FilePageModel>(await _mediator.Send(getFiles));
        }

        public override async Task<File> GetFile(GetFileRequest getFileRequest, ServerCallContext context)
        {
            var getFile = _mapper.Map<GetFileRequest, GetFile>(getFileRequest);
            return _mapper.Map<FileModel, File>(await _mediator.Send(getFile));
        }

        [Authorize("WriteFile")]
        public override async Task<Empty> UpdateFile(UpdateFileRequest updateFileRequest, ServerCallContext context)
        {
            var updateFile = _mapper.Map<UpdateFileRequest, UpdateFile>(updateFileRequest);
            return _mapper.Map<Unit, Empty>(await _mediator.Send(updateFile));
        }

        [Authorize("WriteFile")]
        public override async Task<CreatedEntity> CreateFile(CreateFileRequest createFileRequest, ServerCallContext context)
        {
            var createFile = _mapper.Map<CreateFileRequest, CreateFile>(createFileRequest);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(await _mediator.Send(createFile));
        }

        [Authorize("WriteFile")]
        public override async Task<Empty> DeleteFile(DeleteFileRequest deleteFileRequest, ServerCallContext context)
        {
            var deleteFile = _mapper.Map<DeleteFileRequest, DeleteFile>(deleteFileRequest);
            return _mapper.Map<Unit, Empty>(await _mediator.Send(deleteFile));
        }
    }
}
