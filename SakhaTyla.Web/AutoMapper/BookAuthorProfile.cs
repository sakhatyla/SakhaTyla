using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.BookAuthors;
using SakhaTyla.Core.Requests.BookAuthors.Models;
using SakhaTyla.Web.Protos.BookAuthors;

namespace SakhaTyla.Web.AutoMapper
{
    public class BookAuthorProfile : Profile
    {
        public BookAuthorProfile()
        {
            CreateMap<CreateBookAuthorRequest, CreateBookAuthor>()
                .ForMember(dest => dest.LastName, opt => opt.Condition(src => src.LastNameOneOfCase == CreateBookAuthorRequest.LastNameOneOfOneofCase.LastName))
                .ForMember(dest => dest.FirstName, opt => opt.Condition(src => src.FirstNameOneOfCase == CreateBookAuthorRequest.FirstNameOneOfOneofCase.FirstName))
                .ForMember(dest => dest.MiddleName, opt => opt.Condition(src => src.MiddleNameOneOfCase == CreateBookAuthorRequest.MiddleNameOneOfOneofCase.MiddleName))
                .ForMember(dest => dest.NickName, opt => opt.Condition(src => src.NickNameOneOfCase == CreateBookAuthorRequest.NickNameOneOfOneofCase.NickName));
            CreateMap<DeleteBookAuthorRequest, DeleteBookAuthor>();
            CreateMap<GetBookAuthorRequest, GetBookAuthor>();
            CreateMap<GetBookAuthorsRequest, GetBookAuthors>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetBookAuthorsRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetBookAuthorsRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetBookAuthorsRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateBookAuthorRequest, UpdateBookAuthor>()
                .ForMember(dest => dest.LastName, opt => opt.Condition(src => src.LastNameOneOfCase == UpdateBookAuthorRequest.LastNameOneOfOneofCase.LastName))
                .ForMember(dest => dest.FirstName, opt => opt.Condition(src => src.FirstNameOneOfCase == UpdateBookAuthorRequest.FirstNameOneOfOneofCase.FirstName))
                .ForMember(dest => dest.MiddleName, opt => opt.Condition(src => src.MiddleNameOneOfCase == UpdateBookAuthorRequest.MiddleNameOneOfOneofCase.MiddleName))
                .ForMember(dest => dest.NickName, opt => opt.Condition(src => src.NickNameOneOfCase == UpdateBookAuthorRequest.NickNameOneOfOneofCase.NickName));

            CreateMap<BookAuthorModel, BookAuthor>()
                .ForMember(dest => dest.LastName, opt => opt.Condition(src => src.LastName != default))
                .ForMember(dest => dest.FirstName, opt => opt.Condition(src => src.FirstName != default))
                .ForMember(dest => dest.MiddleName, opt => opt.Condition(src => src.MiddleName != default))
                .ForMember(dest => dest.NickName, opt => opt.Condition(src => src.NickName != default));
            CreateMap<PageModel<BookAuthorModel>, BookAuthorPageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<BookAuthor>>(src.PageItems)));
        }
    }
}
