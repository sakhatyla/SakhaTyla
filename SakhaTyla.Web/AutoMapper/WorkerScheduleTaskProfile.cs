using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using SakhaTyla.Core.Requests.WorkerScheduleTasks;
using SakhaTyla.Core.Requests.WorkerScheduleTasks.Models;
using SakhaTyla.Web.Protos.WorkerScheduleTasks;

namespace SakhaTyla.Web.AutoMapper
{
    public class WorkerScheduleTaskProfile : Profile
    {
        public WorkerScheduleTaskProfile()
        {
            CreateMap<CreateWorkerScheduleTaskRequest, CreateWorkerScheduleTask>()
                .ForMember(dest => dest.WorkerInfoId, opt => opt.Condition(src => src.WorkerInfoIdOneOfCase == CreateWorkerScheduleTaskRequest.WorkerInfoIdOneOfOneofCase.WorkerInfoId))
                .ForMember(dest => dest.Seconds, opt => opt.Condition(src => src.SecondsOneOfCase == CreateWorkerScheduleTaskRequest.SecondsOneOfOneofCase.Seconds))
                .ForMember(dest => dest.Minutes, opt => opt.Condition(src => src.MinutesOneOfCase == CreateWorkerScheduleTaskRequest.MinutesOneOfOneofCase.Minutes))
                .ForMember(dest => dest.Hours, opt => opt.Condition(src => src.HoursOneOfCase == CreateWorkerScheduleTaskRequest.HoursOneOfOneofCase.Hours))
                .ForMember(dest => dest.DayOfMonth, opt => opt.Condition(src => src.DayOfMonthOneOfCase == CreateWorkerScheduleTaskRequest.DayOfMonthOneOfOneofCase.DayOfMonth))
                .ForMember(dest => dest.Month, opt => opt.Condition(src => src.MonthOneOfCase == CreateWorkerScheduleTaskRequest.MonthOneOfOneofCase.Month))
                .ForMember(dest => dest.DayOfWeek, opt => opt.Condition(src => src.DayOfWeekOneOfCase == CreateWorkerScheduleTaskRequest.DayOfWeekOneOfOneofCase.DayOfWeek))
                .ForMember(dest => dest.Year, opt => opt.Condition(src => src.YearOneOfCase == CreateWorkerScheduleTaskRequest.YearOneOfOneofCase.Year));
            CreateMap<DeleteWorkerScheduleTaskRequest, DeleteWorkerScheduleTask>();
            CreateMap<GetWorkerScheduleTaskRequest, GetWorkerScheduleTask>();
            CreateMap<GetWorkerScheduleTasksRequest, GetWorkerScheduleTasks>()
                .ForMember(dest => dest.PageIndex, opt => opt.Condition(src => src.PageIndexOneOfCase == GetWorkerScheduleTasksRequest.PageIndexOneOfOneofCase.PageIndex))
                .ForMember(dest => dest.PageSize, opt => opt.Condition(src => src.PageSizeOneOfCase == GetWorkerScheduleTasksRequest.PageSizeOneOfOneofCase.PageSize))
                .ForMember(dest => dest.OrderDirection, opt => opt.Condition(src => src.OrderDirectionOneOfCase == GetWorkerScheduleTasksRequest.OrderDirectionOneOfOneofCase.OrderDirection));
            CreateMap<UpdateWorkerScheduleTaskRequest, UpdateWorkerScheduleTask>()
                .ForMember(dest => dest.Seconds, opt => opt.Condition(src => src.SecondsOneOfCase == UpdateWorkerScheduleTaskRequest.SecondsOneOfOneofCase.Seconds))
                .ForMember(dest => dest.Minutes, opt => opt.Condition(src => src.MinutesOneOfCase == UpdateWorkerScheduleTaskRequest.MinutesOneOfOneofCase.Minutes))
                .ForMember(dest => dest.Hours, opt => opt.Condition(src => src.HoursOneOfCase == UpdateWorkerScheduleTaskRequest.HoursOneOfOneofCase.Hours))
                .ForMember(dest => dest.DayOfMonth, opt => opt.Condition(src => src.DayOfMonthOneOfCase == UpdateWorkerScheduleTaskRequest.DayOfMonthOneOfOneofCase.DayOfMonth))
                .ForMember(dest => dest.Month, opt => opt.Condition(src => src.MonthOneOfCase == UpdateWorkerScheduleTaskRequest.MonthOneOfOneofCase.Month))
                .ForMember(dest => dest.DayOfWeek, opt => opt.Condition(src => src.DayOfWeekOneOfCase == UpdateWorkerScheduleTaskRequest.DayOfWeekOneOfOneofCase.DayOfWeek))
                .ForMember(dest => dest.Year, opt => opt.Condition(src => src.YearOneOfCase == UpdateWorkerScheduleTaskRequest.YearOneOfOneofCase.Year));

            CreateMap<WorkerScheduleTaskModel, WorkerScheduleTask>()
                .ForMember(dest => dest.WorkerInfoId, opt => opt.Condition(src => src.WorkerInfoId != default))
                .ForMember(dest => dest.Seconds, opt => opt.Condition(src => src.Seconds != default))
                .ForMember(dest => dest.Minutes, opt => opt.Condition(src => src.Minutes != default))
                .ForMember(dest => dest.Hours, opt => opt.Condition(src => src.Hours != default))
                .ForMember(dest => dest.DayOfMonth, opt => opt.Condition(src => src.DayOfMonth != default))
                .ForMember(dest => dest.Month, opt => opt.Condition(src => src.Month != default))
                .ForMember(dest => dest.DayOfWeek, opt => opt.Condition(src => src.DayOfWeek != default))
                .ForMember(dest => dest.Year, opt => opt.Condition(src => src.Year != default));
            CreateMap<PageModel<WorkerScheduleTaskModel>, WorkerScheduleTaskPageModel>()                
                .ForMember(dest => dest.PageItems, opt => opt.Ignore())
                .AfterMap((src, dest, rc) => dest.PageItems.AddRange(rc.Mapper.Map<IEnumerable<WorkerScheduleTask>>(src.PageItems)));
        }
    }
}
