using System;
using System.Linq;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Public.Books.Models;

namespace SakhaTyla.Core.Requests.Public.Books
{
    public static class BookExtensions
    {
        public static IOrderedQueryable<Book> OrderBy(this IQueryable<Book> queryable, string? propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "Name":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Name)
                        : queryable.OrderBy(e => e.Name);
                case "Synonym":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Synonym)
                        : queryable.OrderBy(e => e.Synonym);
                case "Cover":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Cover)
                        : queryable.OrderBy(e => e.Cover);
                case "Id":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Id)
                        : queryable.OrderBy(e => e.Id);
                case "":
                case null:
                    return queryable.OrderBy(e => e.Name);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<Book> Filter(this IQueryable<Book> queryable, BookFilter? filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.Name!.Contains(filter.Text) || e.Synonym!.Contains(filter.Text) || e.Cover!.Contains(filter.Text));
            }
            if (!string.IsNullOrEmpty(filter?.Name))
            {
                queryable = queryable.Where(e => e.Name!.Contains(filter.Name));
            }
            return queryable;
        }
    }
}
