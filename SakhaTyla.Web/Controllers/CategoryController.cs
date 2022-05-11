using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Categories;
using SakhaTyla.Core.Requests.Categories.Models;

namespace SakhaTyla.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize("ReadCategory")]
    [ValidateModel]
    [Route("api")]
    public class CategoryController : Controller
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetCategories")]
        public async Task<PageModel<CategoryModel>> GetCategoriesAsync([FromBody] GetCategories getCategories)
        {
            return await _mediator.Send(getCategories);
        }

        [HttpPost("GetCategory")]
        public async Task<CategoryModel?> GetCategoryAsync([FromBody] GetCategory getCategory)
        {
            return await _mediator.Send(getCategory);
        }

        [HttpPost("ExportCategories")]
        public async Task<FileResult> ExportCategoriesAsync([FromBody] ExportCategories exportCategories)
        {
            var file = await _mediator.Send(exportCategories);
            return File(file.Content, file.ContentType, file.Name);
        }

        [Authorize("WriteCategory")]
        [HttpPost("UpdateCategory")]
        public async Task<Unit> UpdateCategoryAsync([FromBody] UpdateCategory updateCategory)
        {
            return await _mediator.Send(updateCategory);
        }

        [Authorize("WriteCategory")]
        [HttpPost("CreateCategory")]
        public async Task<CreatedEntity<int>> CreateCategoryAsync([FromBody] CreateCategory createCategory)
        {
            return await _mediator.Send(createCategory);
        }

        [Authorize("WriteCategory")]
        [HttpPost("DeleteCategory")]
        public async Task<Unit> DeleteCategoryAsync([FromBody] DeleteCategory deleteCategory)
        {
            return await _mediator.Send(deleteCategory);
        }
    }
}