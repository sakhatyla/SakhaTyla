using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.BookPages;
using SakhaTyla.Core.Requests.BookPages.Models;
using SakhaTyla.Web.Protos.BookPages;

namespace SakhaTyla.Web.AutoMapper
{
    public class BookPageProfile : Profile
    {
        public BookPageProfile()
        {
            CreateMap<CreateBookPageRequest, CreateBookPage>()
                .ForMember(dest => dest.BookId, opt => opt.Condition(src => src.BookIdOneOfCase == CreateBookPageRequest.BookIdOneOfOneofCase.BookId))
                .ForMember(dest => dest.FileName, opt => opt.Condition(src => src.FileNameOneOfCase == CreateBookPageRequest.FileNameOneOfOneofCase.FileName))
                .ForMember(dest => dest.Number, opt => opt.Condition(src => src.NumberOneOfCase == CreateBookPageRequest.NumberOneOfOneofCase.Number));
            CreateMap<DeleteBookPageRequest, DeleteBookPage>();
            CreateMap<GetBookPageRequest, GetBookPage>();
            CreateMap<GetBookPagesRequest, GetBookPages>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetBookPagesRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetBookPagesRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetBookPagesRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateBookPageRequest, UpdateBookPage>()
                .ForMember(dest => dest.FileName, opt => opt.Condition(src => src.FileNameOneOfCase == UpdateBookPageRequest.FileNameOneOfOneofCase.FileName))
                .ForMember(dest => dest.Number, opt => opt.Condition(src => src.NumberOneOfCase == UpdateBookPageRequest.NumberOneOfOneofCase.Number));

            CreateMap<BookPageModel, BookPage>()
                .ForMember(dest => dest.BookId, opt => opt.Condition(src => src.BookId != default))
                .ForMember(dest => dest.FileName, opt => opt.Condition(src => src.FileName != default))
                .ForMember(dest => dest.Number, opt => opt.Condition(src => src.Number != default));
            CreateMap<PageModel<BookPageModel>, BookPagePageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<BookPage>>(src.PageItems)));
        }
    }
}
