using System;
using System.Linq;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Books.Models;

namespace SakhaTyla.Core.Requests.Books
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
                case "Hidden":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Hidden)
                        : queryable.OrderBy(e => e.Hidden);
                case "Cover":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Cover)
                        : queryable.OrderBy(e => e.Cover);
                case "":
                case null:
                    return queryable.OrderBy(e => e.Id);
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
            if (!string.IsNullOrEmpty(filter?.Synonym))
            {
                queryable = queryable.Where(e => e.Synonym!.Contains(filter.Synonym));
            }
            if (filter?.Hidden != null)
            {
                queryable = queryable.Where(e => e.Hidden == filter.Hidden);
            }
            if (!string.IsNullOrEmpty(filter?.Cover))
            {
                queryable = queryable.Where(e => e.Cover!.Contains(filter.Cover));
            }
            return queryable;
        }
    }
}
