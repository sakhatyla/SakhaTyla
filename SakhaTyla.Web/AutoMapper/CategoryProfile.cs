using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.Categories;
using SakhaTyla.Core.Requests.Categories.Models;
using SakhaTyla.Web.Protos.Categories;

namespace SakhaTyla.Web.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategoryRequest, CreateCategory>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == CreateCategoryRequest.NameOneOfOneofCase.Name));
            CreateMap<DeleteCategoryRequest, DeleteCategory>();
            CreateMap<GetCategoryRequest, GetCategory>();
            CreateMap<GetCategoriesRequest, GetCategories>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetCategoriesRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetCategoriesRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetCategoriesRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateCategoryRequest, UpdateCategory>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == UpdateCategoryRequest.NameOneOfOneofCase.Name));

            CreateMap<CategoryModel, Category>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != default));
            CreateMap<PageModel<CategoryModel>, CategoryPageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<Category>>(src.PageItems)));
        }
    }
}
