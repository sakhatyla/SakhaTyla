using AutoMapper;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Requests.Routes;
using SakhaTyla.Core.Requests.Routes.Models;

namespace SakhaTyla.Core.AutoMapper
{
    public class RouteProfile : Profile
    {
        public RouteProfile()
        {
            CreateMap<Route, RouteModel>();
            CreateMap<Route, RouteShortModel>();
            CreateMap<UpdateRoute, Route>();
        }
    }
}
