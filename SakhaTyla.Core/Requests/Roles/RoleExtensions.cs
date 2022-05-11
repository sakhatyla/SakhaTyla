using System;
using System.Linq;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Roles.Models;

namespace SakhaTyla.Core.Requests.Roles
{
    public static class RoleExtensions
    {
        public static IOrderedQueryable<Role> OrderBy(this IQueryable<Role> queryable, string? propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "Name":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Name)
                        : queryable.OrderBy(e => e.Name);
                case "DisplayName":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.DisplayName)
                        : queryable.OrderBy(e => e.DisplayName);
                case "":
                case null:
                    return queryable.OrderBy(e => e.Id);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<Role> Filter(this IQueryable<Role> queryable, RoleFilter? filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.Name.Contains(filter.Text) || e.DisplayName.Contains(filter.Text));
            }
            if (!string.IsNullOrEmpty(filter?.Name))
            {
                queryable = queryable.Where(e => e.Name.Contains(filter.Name));
            }
            if (!string.IsNullOrEmpty(filter?.DisplayName))
            {
                queryable = queryable.Where(e => e.DisplayName.Contains(filter.DisplayName));
            }
            return queryable;
        }
    }
}
