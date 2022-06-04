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
using SakhaTyla.Core.Requests.BookLabels;
using SakhaTyla.Core.Requests.BookLabels.Models;
using SakhaTyla.Web.Protos;
using SakhaTyla.Web.Protos.BookLabels;

namespace SakhaTyla.Web.Services
{
    [Authorize("ReadBookLabel")]
    public class BookLabelService : Protos.BookLabels.BookLabelService.BookLabelServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BookLabelService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<BookLabelPageModel> GetBookLabels(GetBookLabelsRequest getBookLabelsRequest, ServerCallContext context)
        {
            var getBookLabels = _mapper.Map<GetBookLabelsRequest, GetBookLabels>(getBookLabelsRequest);
            var model = await _mediator.Send(getBookLabels);
            return _mapper.Map<PageModel<BookLabelModel>, BookLabelPageModel>(model);
        }

        public override async Task<BookLabel> GetBookLabel(GetBookLabelRequest getBookLabelRequest, ServerCallContext context)
        {
            var getBookLabel = _mapper.Map<GetBookLabelRequest, GetBookLabel>(getBookLabelRequest);
            var model = await _mediator.Send(getBookLabel);
            return _mapper.Map<BookLabelModel, BookLabel>(model!);
        }

        [Authorize("WriteBookLabel")]
        public override async Task<Empty> UpdateBookLabel(UpdateBookLabelRequest updateBookLabelRequest, ServerCallContext context)
        {
            var updateBookLabel = _mapper.Map<UpdateBookLabelRequest, UpdateBookLabel>(updateBookLabelRequest);
            var model = await _mediator.Send(updateBookLabel);
            return _mapper.Map<Unit, Empty>(model);
        }

        [Authorize("WriteBookLabel")]
        public override async Task<CreatedEntity> CreateBookLabel(CreateBookLabelRequest createBookLabelRequest, ServerCallContext context)
        {
            var createBookLabel = _mapper.Map<CreateBookLabelRequest, CreateBookLabel>(createBookLabelRequest);
            var model = await _mediator.Send(createBookLabel);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(model);
        }

        [Authorize("WriteBookLabel")]
        public override async Task<Empty> DeleteBookLabel(DeleteBookLabelRequest deleteBookLabelRequest, ServerCallContext context)
        {
            var deleteBookLabel = _mapper.Map<DeleteBookLabelRequest, DeleteBookLabel>(deleteBookLabelRequest);
            var model = await _mediator.Send(deleteBookLabel);
            return _mapper.Map<Unit, Empty>(model);
        }
    }
}
