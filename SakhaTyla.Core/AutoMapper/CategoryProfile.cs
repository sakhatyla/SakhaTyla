using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Categories;
using SakhaTyla.Core.Requests.Categories.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryModel>();
            CreateMap<Category, CategoryShortModel>();
            CreateMap<CreateCategory, Category>();
            CreateMap<UpdateCategory, Category>();
        }
    }
}
