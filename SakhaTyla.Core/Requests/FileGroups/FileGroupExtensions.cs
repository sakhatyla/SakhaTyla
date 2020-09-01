using System;
using System.Linq;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.FileGroups.Models;

namespace SakhaTyla.Core.Requests.FileGroups
{
    public static class FileGroupExtensions
    {
        public static IOrderedQueryable<FileGroup> OrderBy(this IQueryable<FileGroup> queryable, string propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "Name":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Name)
                        : queryable.OrderBy(e => e.Name);
                case "Type":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Type)
                        : queryable.OrderBy(e => e.Type);
                case "Location":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Location)
                        : queryable.OrderBy(e => e.Location);
                case "Accept":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Accept)
                        : queryable.OrderBy(e => e.Accept);
                case "":
                case null:
                    return queryable.OrderBy(e => e.Id);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<FileGroup> Filter(this IQueryable<FileGroup> queryable, FileGroupFilter filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.Name.Contains(filter.Text) || e.Location.Contains(filter.Text) || e.Accept.Contains(filter.Text));
            }
            if (!string.IsNullOrEmpty(filter?.Name))
            {
                queryable = queryable.Where(e => e.Name.Contains(filter.Name));
            }
            if (filter?.Type != null)
            {
                queryable = queryable.Where(e => e.Type == filter.Type);
            }
            if (!string.IsNullOrEmpty(filter?.Location))
            {
                queryable = queryable.Where(e => e.Location.Contains(filter.Location));
            }
            if (!string.IsNullOrEmpty(filter?.Accept))
            {
                queryable = queryable.Where(e => e.Accept.Contains(filter.Accept));
            }
            return queryable;
        }
    }
}
