using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Widgets;
using SakhaTyla.Core.Requests.Widgets.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class WidgetProfile : Profile
    {
        public WidgetProfile()
        {
            CreateMap<Widget, WidgetModel>();
            CreateMap<Widget, WidgetShortModel>();
            CreateMap<CreateWidget, Widget>();
            CreateMap<UpdateWidget, Widget>();
        }
    }
}
