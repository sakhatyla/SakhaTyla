using System;
using System.Linq;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Categories.Models;

namespace SakhaTyla.Core.Requests.Categories
{
    public static class CategoryExtensions
    {
        public static IOrderedQueryable<Category> OrderBy(this IQueryable<Category> queryable, string? propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "Name":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Name)
                        : queryable.OrderBy(e => e.Name);
                case "":
                case null:
                    return queryable.OrderBy(e => e.Id);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<Category> Filter(this IQueryable<Category> queryable, CategoryFilter? filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.Name!.Contains(filter.Text));
            }
            if (!string.IsNullOrEmpty(filter?.Name))
            {
                queryable = queryable.Where(e => e.Name!.Contains(filter.Name));
            }
            return queryable;
        }
    }
}
