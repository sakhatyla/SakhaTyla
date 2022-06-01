using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.Books;
using SakhaTyla.Core.Requests.Books.Models;
using SakhaTyla.Web.Protos.Books;

namespace SakhaTyla.Web.AutoMapper
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<CreateBookRequest, CreateBook>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == CreateBookRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.Synonym, opt => opt.Condition(src => src.SynonymOneOfCase == CreateBookRequest.SynonymOneOfOneofCase.Synonym))
                .ForMember(dest => dest.Hidden, opt => opt.Condition(src => src.HiddenOneOfCase == CreateBookRequest.HiddenOneOfOneofCase.Hidden))
                .ForMember(dest => dest.Cover, opt => opt.Condition(src => src.CoverOneOfCase == CreateBookRequest.CoverOneOfOneofCase.Cover));
            CreateMap<DeleteBookRequest, DeleteBook>();
            CreateMap<GetBookRequest, GetBook>();
            CreateMap<GetBooksRequest, GetBooks>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetBooksRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetBooksRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetBooksRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateBookRequest, UpdateBook>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == UpdateBookRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.Synonym, opt => opt.Condition(src => src.SynonymOneOfCase == UpdateBookRequest.SynonymOneOfOneofCase.Synonym))
                .ForMember(dest => dest.Hidden, opt => opt.Condition(src => src.HiddenOneOfCase == UpdateBookRequest.HiddenOneOfOneofCase.Hidden))
                .ForMember(dest => dest.Cover, opt => opt.Condition(src => src.CoverOneOfCase == UpdateBookRequest.CoverOneOfOneofCase.Cover));

            CreateMap<BookModel, Book>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != default))
                .ForMember(dest => dest.Synonym, opt => opt.Condition(src => src.Synonym != default))
                .ForMember(dest => dest.Hidden, opt => opt.Condition(src => src.Hidden != default))
                .ForMember(dest => dest.Cover, opt => opt.Condition(src => src.Cover != default));
            CreateMap<PageModel<BookModel>, BookPageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<Book>>(src.PageItems)));
        }
    }
}
