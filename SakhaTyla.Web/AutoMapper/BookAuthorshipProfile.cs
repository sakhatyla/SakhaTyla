using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.BookAuthorships;
using SakhaTyla.Core.Requests.BookAuthorships.Models;
using SakhaTyla.Web.Protos.BookAuthorships;

namespace SakhaTyla.Web.AutoMapper
{
    public class BookAuthorshipProfile : Profile
    {
        public BookAuthorshipProfile()
        {
            CreateMap<CreateBookAuthorshipRequest, CreateBookAuthorship>()
                .ForMember(dest => dest.BookId, opt => opt.Condition(src => src.BookIdOneOfCase == CreateBookAuthorshipRequest.BookIdOneOfOneofCase.BookId))
                .ForMember(dest => dest.AuthorId, opt => opt.Condition(src => src.AuthorIdOneOfCase == CreateBookAuthorshipRequest.AuthorIdOneOfOneofCase.AuthorId))
                .ForMember(dest => dest.Weight, opt => opt.Condition(src => src.WeightOneOfCase == CreateBookAuthorshipRequest.WeightOneOfOneofCase.Weight));
            CreateMap<DeleteBookAuthorshipRequest, DeleteBookAuthorship>();
            CreateMap<GetBookAuthorshipRequest, GetBookAuthorship>();
            CreateMap<GetBookAuthorshipsRequest, GetBookAuthorships>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetBookAuthorshipsRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetBookAuthorshipsRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetBookAuthorshipsRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateBookAuthorshipRequest, UpdateBookAuthorship>()
                .ForMember(dest => dest.AuthorId, opt => opt.Condition(src => src.AuthorIdOneOfCase == UpdateBookAuthorshipRequest.AuthorIdOneOfOneofCase.AuthorId))
                .ForMember(dest => dest.Weight, opt => opt.Condition(src => src.WeightOneOfCase == UpdateBookAuthorshipRequest.WeightOneOfOneofCase.Weight));

            CreateMap<BookAuthorshipModel, BookAuthorship>()
                .ForMember(dest => dest.BookId, opt => opt.Condition(src => src.BookId != default))
                .ForMember(dest => dest.AuthorId, opt => opt.Condition(src => src.AuthorId != default))
                .ForMember(dest => dest.Weight, opt => opt.Condition(src => src.Weight != default));
            CreateMap<PageModel<BookAuthorshipModel>, BookAuthorshipPageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<BookAuthorship>>(src.PageItems)));
        }
    }
}
