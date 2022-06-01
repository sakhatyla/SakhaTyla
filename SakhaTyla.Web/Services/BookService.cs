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
using SakhaTyla.Core.Requests.Books;
using SakhaTyla.Core.Requests.Books.Models;
using SakhaTyla.Web.Protos;
using SakhaTyla.Web.Protos.Books;

namespace SakhaTyla.Web.Services
{
    [Authorize("ReadBook")]
    public class BookService : Protos.Books.BookService.BookServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BookService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<BookPageModel> GetBooks(GetBooksRequest getBooksRequest, ServerCallContext context)
        {
            var getBooks = _mapper.Map<GetBooksRequest, GetBooks>(getBooksRequest);
            var model = await _mediator.Send(getBooks);
            return _mapper.Map<PageModel<BookModel>, BookPageModel>(model);
        }

        public override async Task<Book> GetBook(GetBookRequest getBookRequest, ServerCallContext context)
        {
            var getBook = _mapper.Map<GetBookRequest, GetBook>(getBookRequest);
            var model = await _mediator.Send(getBook);
            return _mapper.Map<BookModel, Book>(model!);
        }

        [Authorize("WriteBook")]
        public override async Task<Empty> UpdateBook(UpdateBookRequest updateBookRequest, ServerCallContext context)
        {
            var updateBook = _mapper.Map<UpdateBookRequest, UpdateBook>(updateBookRequest);
            var model = await _mediator.Send(updateBook);
            return _mapper.Map<Unit, Empty>(model);
        }

        [Authorize("WriteBook")]
        public override async Task<CreatedEntity> CreateBook(CreateBookRequest createBookRequest, ServerCallContext context)
        {
            var createBook = _mapper.Map<CreateBookRequest, CreateBook>(createBookRequest);
            var model = await _mediator.Send(createBook);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(model);
        }

        [Authorize("WriteBook")]
        public override async Task<Empty> DeleteBook(DeleteBookRequest deleteBookRequest, ServerCallContext context)
        {
            var deleteBook = _mapper.Map<DeleteBookRequest, DeleteBook>(deleteBookRequest);
            var model = await _mediator.Send(deleteBook);
            return _mapper.Map<Unit, Empty>(model);
        }
    }
}
