using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.BookLabels;
using SakhaTyla.Core.Requests.BookLabels.Models;
using SakhaTyla.Web.Protos.BookLabels;

namespace SakhaTyla.Web.AutoMapper
{
    public class BookLabelProfile : Profile
    {
        public BookLabelProfile()
        {
            CreateMap<CreateBookLabelRequest, CreateBookLabel>()
                .ForMember(dest => dest.BookId, opt => opt.Condition(src => src.BookIdOneOfCase == CreateBookLabelRequest.BookIdOneOfOneofCase.BookId))
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == CreateBookLabelRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.PageId, opt => opt.Condition(src => src.PageIdOneOfCase == CreateBookLabelRequest.PageIdOneOfOneofCase.PageId));
            CreateMap<DeleteBookLabelRequest, DeleteBookLabel>();
            CreateMap<GetBookLabelRequest, GetBookLabel>();
            CreateMap<GetBookLabelsRequest, GetBookLabels>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetBookLabelsRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetBookLabelsRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetBookLabelsRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateBookLabelRequest, UpdateBookLabel>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == UpdateBookLabelRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.PageId, opt => opt.Condition(src => src.PageIdOneOfCase == UpdateBookLabelRequest.PageIdOneOfOneofCase.PageId));

            CreateMap<BookLabelModel, BookLabel>()
                .ForMember(dest => dest.BookId, opt => opt.Condition(src => src.BookId != default))
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != default))
                .ForMember(dest => dest.PageId, opt => opt.Condition(src => src.PageId != default));
            CreateMap<PageModel<BookLabelModel>, BookLabelPageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<BookLabel>>(src.PageItems)));
        }
    }
}
