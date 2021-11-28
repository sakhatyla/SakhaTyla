using System;
using System.Linq;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.WorkerRuns.Models;

namespace SakhaTyla.Core.Requests.WorkerRuns
{
    public static class WorkerRunExtensions
    {
        public static IOrderedQueryable<WorkerRun> OrderBy(this IQueryable<WorkerRun> queryable, string propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "WorkerInfo":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.WorkerInfo)
                        : queryable.OrderBy(e => e.WorkerInfo);
                case "Status":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Status)
                        : queryable.OrderBy(e => e.Status);
                case "StartDateTime":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.StartDateTime)
                        : queryable.OrderBy(e => e.StartDateTime);
                case "EndDateTime":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.EndDateTime)
                        : queryable.OrderBy(e => e.EndDateTime);
                case "Data":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Data)
                        : queryable.OrderBy(e => e.Data);
                case "Result":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Result)
                        : queryable.OrderBy(e => e.Result);
                case "ResultData":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.ResultData)
                        : queryable.OrderBy(e => e.ResultData);
                case "":
                case null:
                    return queryable.OrderByDescending(e => e.Id);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<WorkerRun> Filter(this IQueryable<WorkerRun> queryable, WorkerRunFilter filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.Data.Contains(filter.Text) || e.Result.Contains(filter.Text) || e.ResultData.Contains(filter.Text));
            }
            if (filter?.WorkerInfoId != null)
            {
                queryable = queryable.Where(e => e.WorkerInfoId == filter.WorkerInfoId);
            }
            if (filter?.Status != null)
            {
                queryable = queryable.Where(e => e.Status == filter.Status);
            }
            if (filter?.StartDateTimeFrom != null)
            {
                queryable = queryable.Where(e => e.StartDateTime >= filter.StartDateTimeFrom);
            }
            if (filter?.StartDateTimeTo != null)
            {
                queryable = queryable.Where(e => e.StartDateTime <= filter.StartDateTimeTo);
            }
            if (filter?.EndDateTimeFrom != null)
            {
                queryable = queryable.Where(e => e.EndDateTime >= filter.EndDateTimeFrom);
            }
            if (filter?.EndDateTimeTo != null)
            {
                queryable = queryable.Where(e => e.EndDateTime <= filter.EndDateTimeTo);
            }
            if (!string.IsNullOrEmpty(filter?.Data))
            {
                queryable = queryable.Where(e => e.Data.Contains(filter.Data));
            }
            if (!string.IsNullOrEmpty(filter?.Result))
            {
                queryable = queryable.Where(e => e.Result.Contains(filter.Result));
            }
            if (!string.IsNullOrEmpty(filter?.ResultData))
            {
                queryable = queryable.Where(e => e.ResultData.Contains(filter.ResultData));
            }
            return queryable;
        }
    }
}
