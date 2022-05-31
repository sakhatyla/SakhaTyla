using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.Widgets;
using SakhaTyla.Core.Requests.Widgets.Models;
using SakhaTyla.Web.Protos.Widgets;

namespace SakhaTyla.Web.AutoMapper
{
    public class WidgetProfile : Profile
    {
        public WidgetProfile()
        {
            CreateMap<CreateWidgetRequest, CreateWidget>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == CreateWidgetRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.Code, opt => opt.Condition(src => src.CodeOneOfCase == CreateWidgetRequest.CodeOneOfOneofCase.Code))
                .ForMember(dest => dest.Body, opt => opt.Condition(src => src.BodyOneOfCase == CreateWidgetRequest.BodyOneOfOneofCase.Body))
                .ForMember(dest => dest.Type, opt => opt.Condition(src => src.TypeOneOfCase == CreateWidgetRequest.TypeOneOfOneofCase.Type));
            CreateMap<DeleteWidgetRequest, DeleteWidget>();
            CreateMap<GetWidgetRequest, GetWidget>();
            CreateMap<GetWidgetsRequest, GetWidgets>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetWidgetsRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetWidgetsRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetWidgetsRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateWidgetRequest, UpdateWidget>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.NameOneOfCase == UpdateWidgetRequest.NameOneOfOneofCase.Name))
                .ForMember(dest => dest.Code, opt => opt.Condition(src => src.CodeOneOfCase == UpdateWidgetRequest.CodeOneOfOneofCase.Code))
                .ForMember(dest => dest.Body, opt => opt.Condition(src => src.BodyOneOfCase == UpdateWidgetRequest.BodyOneOfOneofCase.Body))
                .ForMember(dest => dest.Type, opt => opt.Condition(src => src.TypeOneOfCase == UpdateWidgetRequest.TypeOneOfOneofCase.Type));

            CreateMap<WidgetModel, Widget>()
                .ForMember(dest => dest.Name, opt => opt.Condition(src => src.Name != default))
                .ForMember(dest => dest.Code, opt => opt.Condition(src => src.Code != default))
                .ForMember(dest => dest.Body, opt => opt.Condition(src => src.Body != default))
                .ForMember(dest => dest.Type, opt => opt.Condition(src => src.Type != default));
            CreateMap<PageModel<WidgetModel>, WidgetPageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<Widget>>(src.PageItems)));
        }
    }
}
