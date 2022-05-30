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
using SakhaTyla.Core.Requests.Pages;
using SakhaTyla.Core.Requests.Pages.Models;
using SakhaTyla.Web.Protos;
using SakhaTyla.Web.Protos.Pages;

namespace SakhaTyla.Web.Services
{
    [Authorize("ReadPage")]
    public class PageService : Protos.Pages.PageService.PageServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PageService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<PagePageModel> GetPages(GetPagesRequest getPagesRequest, ServerCallContext context)
        {
            var getPages = _mapper.Map<GetPagesRequest, GetPages>(getPagesRequest);
            var model = await _mediator.Send(getPages);
            return _mapper.Map<PageModel<PageModel>, PagePageModel>(model);
        }

        public override async Task<Page> GetPage(GetPageRequest getPageRequest, ServerCallContext context)
        {
            var getPage = _mapper.Map<GetPageRequest, GetPage>(getPageRequest);
            var model = await _mediator.Send(getPage);
            return _mapper.Map<PageModel, Page>(model!);
        }

        [Authorize("WritePage")]
        public override async Task<Empty> UpdatePage(UpdatePageRequest updatePageRequest, ServerCallContext context)
        {
            var updatePage = _mapper.Map<UpdatePageRequest, UpdatePage>(updatePageRequest);
            var model = await _mediator.Send(updatePage);
            return _mapper.Map<Unit, Empty>(model);
        }

        [Authorize("WritePage")]
        public override async Task<CreatedEntity> CreatePage(CreatePageRequest createPageRequest, ServerCallContext context)
        {
            var createPage = _mapper.Map<CreatePageRequest, CreatePage>(createPageRequest);
            var model = await _mediator.Send(createPage);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(model);
        }

        [Authorize("WritePage")]
        public override async Task<Empty> DeletePage(DeletePageRequest deletePageRequest, ServerCallContext context)
        {
            var deletePage = _mapper.Map<DeletePageRequest, DeletePage>(deletePageRequest);
            var model = await _mediator.Send(deletePage);
            return _mapper.Map<Unit, Empty>(model);
        }
    }
}
