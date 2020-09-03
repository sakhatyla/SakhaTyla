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
using SakhaTyla.Core.Requests.Categories;
using SakhaTyla.Core.Requests.Categories.Models;
using SakhaTyla.Web.Protos;
using SakhaTyla.Web.Protos.Categories;

namespace SakhaTyla.Web.Services
{
    [Authorize("ReadCategory")]
    public class CategoryService : Protos.Categories.CategoryService.CategoryServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoryService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<CategoryPageModel> GetCategories(GetCategoriesRequest getCategoriesRequest, ServerCallContext context)
        {
            var getCategories = _mapper.Map<GetCategoriesRequest, GetCategories>(getCategoriesRequest);
            return _mapper.Map<PageModel<CategoryModel>, CategoryPageModel>(await _mediator.Send(getCategories));
        }

        public override async Task<Category> GetCategory(GetCategoryRequest getCategoryRequest, ServerCallContext context)
        {
            var getCategory = _mapper.Map<GetCategoryRequest, GetCategory>(getCategoryRequest);
            return _mapper.Map<CategoryModel, Category>(await _mediator.Send(getCategory));
        }

        [Authorize("WriteCategory")]
        public override async Task<Empty> UpdateCategory(UpdateCategoryRequest updateCategoryRequest, ServerCallContext context)
        {
            var updateCategory = _mapper.Map<UpdateCategoryRequest, UpdateCategory>(updateCategoryRequest);
            return _mapper.Map<Unit, Empty>(await _mediator.Send(updateCategory));
        }

        [Authorize("WriteCategory")]
        public override async Task<CreatedEntity> CreateCategory(CreateCategoryRequest createCategoryRequest, ServerCallContext context)
        {
            var createCategory = _mapper.Map<CreateCategoryRequest, CreateCategory>(createCategoryRequest);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(await _mediator.Send(createCategory));
        }

        [Authorize("WriteCategory")]
        public override async Task<Empty> DeleteCategory(DeleteCategoryRequest deleteCategoryRequest, ServerCallContext context)
        {
            var deleteCategory = _mapper.Map<DeleteCategoryRequest, DeleteCategory>(deleteCategoryRequest);
            return _mapper.Map<Unit, Empty>(await _mediator.Send(deleteCategory));
        }
    }
}
