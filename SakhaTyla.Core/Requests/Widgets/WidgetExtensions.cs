using System;
using System.Linq;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Widgets.Models;

namespace SakhaTyla.Core.Requests.Widgets
{
    public static class WidgetExtensions
    {
        public static IOrderedQueryable<Widget> OrderBy(this IQueryable<Widget> queryable, string? propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "Name":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Name)
                        : queryable.OrderBy(e => e.Name);
                case "Code":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Code)
                        : queryable.OrderBy(e => e.Code);
                case "Body":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Body)
                        : queryable.OrderBy(e => e.Body);
                case "Type":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Type)
                        : queryable.OrderBy(e => e.Type);
                case "":
                case null:
                    return queryable.OrderBy(e => e.Id);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<Widget> Filter(this IQueryable<Widget> queryable, WidgetFilter? filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.Name!.Contains(filter.Text) || e.Code!.Contains(filter.Text) || e.Body!.Contains(filter.Text));
            }
            if (!string.IsNullOrEmpty(filter?.Name))
            {
                queryable = queryable.Where(e => e.Name!.Contains(filter.Name));
            }
            if (!string.IsNullOrEmpty(filter?.Code))
            {
                queryable = queryable.Where(e => e.Code!.Contains(filter.Code));
            }
            if (!string.IsNullOrEmpty(filter?.Body))
            {
                queryable = queryable.Where(e => e.Body!.Contains(filter.Body));
            }
            if (filter?.Type != null)
            {
                queryable = queryable.Where(e => e.Type == filter.Type);
            }
            return queryable;
        }
    }
}
