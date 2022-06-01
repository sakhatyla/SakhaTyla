using System;
using System.Linq;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Menus.Models;

namespace SakhaTyla.Core.Requests.Menus
{
    public static class MenuExtensions
    {
        public static IOrderedQueryable<Menu> OrderBy(this IQueryable<Menu> queryable, string? propertyName, OrderDirection? direction)
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
                case "":
                case null:
                    return queryable.OrderBy(e => e.Id);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<Menu> Filter(this IQueryable<Menu> queryable, MenuFilter? filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.Name!.Contains(filter.Text) || e.Code!.Contains(filter.Text));
            }
            if (!string.IsNullOrEmpty(filter?.Name))
            {
                queryable = queryable.Where(e => e.Name!.Contains(filter.Name));
            }
            if (!string.IsNullOrEmpty(filter?.Code))
            {
                queryable = queryable.Where(e => e.Code!.Contains(filter.Code));
            }
            return queryable;
        }
    }
}
