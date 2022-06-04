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
using SakhaTyla.Core.Requests.BookAuthors;
using SakhaTyla.Core.Requests.BookAuthors.Models;
using SakhaTyla.Web.Protos;
using SakhaTyla.Web.Protos.BookAuthors;

namespace SakhaTyla.Web.Services
{
    [Authorize("ReadBookAuthor")]
    public class BookAuthorService : Protos.BookAuthors.BookAuthorService.BookAuthorServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BookAuthorService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<BookAuthorPageModel> GetBookAuthors(GetBookAuthorsRequest getBookAuthorsRequest, ServerCallContext context)
        {
            var getBookAuthors = _mapper.Map<GetBookAuthorsRequest, GetBookAuthors>(getBookAuthorsRequest);
            var model = await _mediator.Send(getBookAuthors);
            return _mapper.Map<PageModel<BookAuthorModel>, BookAuthorPageModel>(model);
        }

        public override async Task<BookAuthor> GetBookAuthor(GetBookAuthorRequest getBookAuthorRequest, ServerCallContext context)
        {
            var getBookAuthor = _mapper.Map<GetBookAuthorRequest, GetBookAuthor>(getBookAuthorRequest);
            var model = await _mediator.Send(getBookAuthor);
            return _mapper.Map<BookAuthorModel, BookAuthor>(model!);
        }

        [Authorize("WriteBookAuthor")]
        public override async Task<Empty> UpdateBookAuthor(UpdateBookAuthorRequest updateBookAuthorRequest, ServerCallContext context)
        {
            var updateBookAuthor = _mapper.Map<UpdateBookAuthorRequest, UpdateBookAuthor>(updateBookAuthorRequest);
            var model = await _mediator.Send(updateBookAuthor);
            return _mapper.Map<Unit, Empty>(model);
        }

        [Authorize("WriteBookAuthor")]
        public override async Task<CreatedEntity> CreateBookAuthor(CreateBookAuthorRequest createBookAuthorRequest, ServerCallContext context)
        {
            var createBookAuthor = _mapper.Map<CreateBookAuthorRequest, CreateBookAuthor>(createBookAuthorRequest);
            var model = await _mediator.Send(createBookAuthor);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(model);
        }

        [Authorize("WriteBookAuthor")]
        public override async Task<Empty> DeleteBookAuthor(DeleteBookAuthorRequest deleteBookAuthorRequest, ServerCallContext context)
        {
            var deleteBookAuthor = _mapper.Map<DeleteBookAuthorRequest, DeleteBookAuthor>(deleteBookAuthorRequest);
            var model = await _mediator.Send(deleteBookAuthor);
            return _mapper.Map<Unit, Empty>(model);
        }
    }
}
