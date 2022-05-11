using System;
using System.Linq;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.WorkerScheduleTasks.Models;

namespace SakhaTyla.Core.Requests.WorkerScheduleTasks
{
    public static class WorkerScheduleTaskExtensions
    {
        public static IOrderedQueryable<WorkerScheduleTask> OrderBy(this IQueryable<WorkerScheduleTask> queryable, string? propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "WorkerInfo":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.WorkerInfo.Name)
                        : queryable.OrderBy(e => e.WorkerInfo.Name);
                case "Seconds":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Seconds)
                        : queryable.OrderBy(e => e.Seconds);
                case "Minutes":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Minutes)
                        : queryable.OrderBy(e => e.Minutes);
                case "Hours":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Hours)
                        : queryable.OrderBy(e => e.Hours);
                case "DayOfMonth":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.DayOfMonth)
                        : queryable.OrderBy(e => e.DayOfMonth);
                case "Month":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Month)
                        : queryable.OrderBy(e => e.Month);
                case "DayOfWeek":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.DayOfWeek)
                        : queryable.OrderBy(e => e.DayOfWeek);
                case "Year":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Year)
                        : queryable.OrderBy(e => e.Year);
                case "":
                case null:
                    return queryable.OrderBy(e => e.Id);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<WorkerScheduleTask> Filter(this IQueryable<WorkerScheduleTask> queryable, WorkerScheduleTaskFilter? filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.Seconds!.Contains(filter.Text) || e.Minutes!.Contains(filter.Text) || e.Hours!.Contains(filter.Text) || e.DayOfMonth!.Contains(filter.Text) || e.Month!.Contains(filter.Text) || e.DayOfWeek!.Contains(filter.Text) || e.Year!.Contains(filter.Text));
            }
            if (filter?.WorkerInfoId != null)
            {
                queryable = queryable.Where(e => e.WorkerInfoId == filter.WorkerInfoId);
            }
            if (!string.IsNullOrEmpty(filter?.Seconds))
            {
                queryable = queryable.Where(e => e.Seconds!.Contains(filter.Seconds));
            }
            if (!string.IsNullOrEmpty(filter?.Minutes))
            {
                queryable = queryable.Where(e => e.Minutes!.Contains(filter.Minutes));
            }
            if (!string.IsNullOrEmpty(filter?.Hours))
            {
                queryable = queryable.Where(e => e.Hours!.Contains(filter.Hours));
            }
            if (!string.IsNullOrEmpty(filter?.DayOfMonth))
            {
                queryable = queryable.Where(e => e.DayOfMonth!.Contains(filter.DayOfMonth));
            }
            if (!string.IsNullOrEmpty(filter?.Month))
            {
                queryable = queryable.Where(e => e.Month!.Contains(filter.Month));
            }
            if (!string.IsNullOrEmpty(filter?.DayOfWeek))
            {
                queryable = queryable.Where(e => e.DayOfWeek!.Contains(filter.DayOfWeek));
            }
            if (!string.IsNullOrEmpty(filter?.Year))
            {
                queryable = queryable.Where(e => e.Year!.Contains(filter.Year));
            }
            return queryable;
        }
    }
}
