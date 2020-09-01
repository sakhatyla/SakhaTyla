using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Web.Protos;

namespace SakhaTyla.Web.AutoMapper
{
    public class CommonProfile : Profile
    {
        public CommonProfile()
        {
            CreateMap<Unit, Empty>();
            CreateMap<CreatedEntity<int>, CreatedEntity>();
            CreateMap<DateTime, Google.Protobuf.WellKnownTypes.Timestamp>().ConvertUsing(src => Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(src));
            CreateMap<Google.Protobuf.WellKnownTypes.Timestamp, DateTime>().ConvertUsing(src => src.ToDateTime());
            CreateMap<TimeSpan, Google.Protobuf.WellKnownTypes.Duration>().ConvertUsing(src => Google.Protobuf.WellKnownTypes.Duration.FromTimeSpan(src));            
            CreateMap<Google.Protobuf.WellKnownTypes.Duration, TimeSpan>().ConvertUsing(src => src.ToTimeSpan());
        }
    }
}
