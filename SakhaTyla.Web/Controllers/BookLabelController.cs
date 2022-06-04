using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.BookLabels;
using SakhaTyla.Core.Requests.BookLabels.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadBookLabel")]
    [ValidateModel]
    [Route("api")]
    public class BookLabelController : Controller
    {
        private readonly IMediator _mediator;

        public BookLabelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetBookLabels")]
        public async Task<PageModel<BookLabelModel>> GetBookLabelsAsync([FromBody] GetBookLabels getBookLabels)
        {
            return await _mediator.Send(getBookLabels);
        }

        [HttpPost("GetBookLabel")]
        public async Task<BookLabelModel?> GetBookLabelAsync([FromBody] GetBookLabel getBookLabel)
        {
            return await _mediator.Send(getBookLabel);
        }

        [HttpPost("ExportBookLabels")]
        public async Task<FileResult> ExportBookLabelsAsync([FromBody] ExportBookLabels exportBookLabels)
        {
            var file = await _mediator.Send(exportBookLabels);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteBookLabel")]
        [HttpPost("UpdateBookLabel")]
        public async Task<Unit> UpdateBookLabelAsync([FromBody] UpdateBookLabel updateBookLabel)
        {
            return await _mediator.Send(updateBookLabel);
        }

        [Authorize("WriteBookLabel")]
        [HttpPost("CreateBookLabel")]
        public async Task<CreatedEntity<int>> CreateBookLabelAsync([FromBody] CreateBookLabel createBookLabel)
        {
            return await _mediator.Send(createBookLabel);
        }

        [Authorize("WriteBookLabel")]
        [HttpPost("DeleteBookLabel")]
        public async Task<Unit> DeleteBookLabelAsync([FromBody] DeleteBookLabel deleteBookLabel)
        {
            return await _mediator.Send(deleteBookLabel);
        }
    }
}