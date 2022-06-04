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
using SakhaTyla.Core.Requests.BookAuthorships;
using SakhaTyla.Core.Requests.BookAuthorships.Models;
using SakhaTyla.Web.Protos;
using SakhaTyla.Web.Protos.BookAuthorships;

namespace SakhaTyla.Web.Services
{
    [Authorize("ReadBookAuthorship")]
    public class BookAuthorshipService : Protos.BookAuthorships.BookAuthorshipService.BookAuthorshipServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BookAuthorshipService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<BookAuthorshipPageModel> GetBookAuthorships(GetBookAuthorshipsRequest getBookAuthorshipsRequest, ServerCallContext context)
        {
            var getBookAuthorships = _mapper.Map<GetBookAuthorshipsRequest, GetBookAuthorships>(getBookAuthorshipsRequest);
            var model = await _mediator.Send(getBookAuthorships);
            return _mapper.Map<PageModel<BookAuthorshipModel>, BookAuthorshipPageModel>(model);
        }

        public override async Task<BookAuthorship> GetBookAuthorship(GetBookAuthorshipRequest getBookAuthorshipRequest, ServerCallContext context)
        {
            var getBookAuthorship = _mapper.Map<GetBookAuthorshipRequest, GetBookAuthorship>(getBookAuthorshipRequest);
            var model = await _mediator.Send(getBookAuthorship);
            return _mapper.Map<BookAuthorshipModel, BookAuthorship>(model!);
        }

        [Authorize("WriteBookAuthorship")]
        public override async Task<Empty> UpdateBookAuthorship(UpdateBookAuthorshipRequest updateBookAuthorshipRequest, ServerCallContext context)
        {
            var updateBookAuthorship = _mapper.Map<UpdateBookAuthorshipRequest, UpdateBookAuthorship>(updateBookAuthorshipRequest);
            var model = await _mediator.Send(updateBookAuthorship);
            return _mapper.Map<Unit, Empty>(model);
        }

        [Authorize("WriteBookAuthorship")]
        public override async Task<CreatedEntity> CreateBookAuthorship(CreateBookAuthorshipRequest createBookAuthorshipRequest, ServerCallContext context)
        {
            var createBookAuthorship = _mapper.Map<CreateBookAuthorshipRequest, CreateBookAuthorship>(createBookAuthorshipRequest);
            var model = await _mediator.Send(createBookAuthorship);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(model);
        }

        [Authorize("WriteBookAuthorship")]
        public override async Task<Empty> DeleteBookAuthorship(DeleteBookAuthorshipRequest deleteBookAuthorshipRequest, ServerCallContext context)
        {
            var deleteBookAuthorship = _mapper.Map<DeleteBookAuthorshipRequest, DeleteBookAuthorship>(deleteBookAuthorshipRequest);
            var model = await _mediator.Send(deleteBookAuthorship);
            return _mapper.Map<Unit, Empty>(model);
        }
    }
}
