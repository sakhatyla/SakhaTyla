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
using SakhaTyla.Core.Requests.Tags;
using SakhaTyla.Core.Requests.Tags.Models;
using SakhaTyla.Web.Protos;
using SakhaTyla.Web.Protos.Tags;

namespace SakhaTyla.Web.Services
{
    [Authorize("ReadTag")]
    public class TagService : Protos.Tags.TagService.TagServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TagService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<TagPageModel> GetTags(GetTagsRequest getTagsRequest, ServerCallContext context)
        {
            var getTags = _mapper.Map<GetTagsRequest, GetTags>(getTagsRequest);
            var model = await _mediator.Send(getTags);
            return _mapper.Map<PageModel<TagModel>, TagPageModel>(model);
        }

        public override async Task<Tag> GetTag(GetTagRequest getTagRequest, ServerCallContext context)
        {
            var getTag = _mapper.Map<GetTagRequest, GetTag>(getTagRequest);
            var model = await _mediator.Send(getTag);
            return _mapper.Map<TagModel, Tag>(model!);
        }

        [Authorize("WriteTag")]
        public override async Task<Empty> UpdateTag(UpdateTagRequest updateTagRequest, ServerCallContext context)
        {
            var updateTag = _mapper.Map<UpdateTagRequest, UpdateTag>(updateTagRequest);
            var model = await _mediator.Send(updateTag);
            return _mapper.Map<Unit, Empty>(model);
        }

        [Authorize("WriteTag")]
        public override async Task<CreatedEntity> CreateTag(CreateTagRequest createTagRequest, ServerCallContext context)
        {
            var createTag = _mapper.Map<CreateTagRequest, CreateTag>(createTagRequest);
            var model = await _mediator.Send(createTag);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(model);
        }

        [Authorize("WriteTag")]
        public override async Task<Empty> DeleteTag(DeleteTagRequest deleteTagRequest, ServerCallContext context)
        {
            var deleteTag = _mapper.Map<DeleteTagRequest, DeleteTag>(deleteTagRequest);
            var model = await _mediator.Send(deleteTag);
            return _mapper.Map<Unit, Empty>(model);
        }
    }
}
