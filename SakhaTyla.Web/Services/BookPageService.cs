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
using SakhaTyla.Core.Requests.BookPages;
using SakhaTyla.Core.Requests.BookPages.Models;
using SakhaTyla.Web.Protos;
using SakhaTyla.Web.Protos.BookPages;

namespace SakhaTyla.Web.Services
{
    [Authorize("ReadBookPage")]
    public class BookPageService : Protos.BookPages.BookPageService.BookPageServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BookPageService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<BookPagePageModel> GetBookPages(GetBookPagesRequest getBookPagesRequest, ServerCallContext context)
        {
            var getBookPages = _mapper.Map<GetBookPagesRequest, GetBookPages>(getBookPagesRequest);
            var model = await _mediator.Send(getBookPages);
            return _mapper.Map<PageModel<BookPageModel>, BookPagePageModel>(model);
        }

        public override async Task<BookPage> GetBookPage(GetBookPageRequest getBookPageRequest, ServerCallContext context)
        {
            var getBookPage = _mapper.Map<GetBookPageRequest, GetBookPage>(getBookPageRequest);
            var model = await _mediator.Send(getBookPage);
            return _mapper.Map<BookPageModel, BookPage>(model!);
        }

        [Authorize("WriteBookPage")]
        public override async Task<Empty> UpdateBookPage(UpdateBookPageRequest updateBookPageRequest, ServerCallContext context)
        {
            var updateBookPage = _mapper.Map<UpdateBookPageRequest, UpdateBookPage>(updateBookPageRequest);
            var model = await _mediator.Send(updateBookPage);
            return _mapper.Map<Unit, Empty>(model);
        }

        [Authorize("WriteBookPage")]
        public override async Task<CreatedEntity> CreateBookPage(CreateBookPageRequest createBookPageRequest, ServerCallContext context)
        {
            var createBookPage = _mapper.Map<CreateBookPageRequest, CreateBookPage>(createBookPageRequest);
            var model = await _mediator.Send(createBookPage);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(model);
        }

        [Authorize("WriteBookPage")]
        public override async Task<Empty> DeleteBookPage(DeleteBookPageRequest deleteBookPageRequest, ServerCallContext context)
        {
            var deleteBookPage = _mapper.Map<DeleteBookPageRequest, DeleteBookPage>(deleteBookPageRequest);
            var model = await _mediator.Send(deleteBookPage);
            return _mapper.Map<Unit, Empty>(model);
        }
    }
}
