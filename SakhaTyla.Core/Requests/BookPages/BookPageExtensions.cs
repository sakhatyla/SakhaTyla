using System;
using System.Linq;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.BookPages.Models;

namespace SakhaTyla.Core.Requests.BookPages
{
    public static class BookPageExtensions
    {
        public static IOrderedQueryable<BookPage> OrderBy(this IQueryable<BookPage> queryable, string? propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "Book":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Book.Name)
                        : queryable.OrderBy(e => e.Book.Name);
                case "FileName":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.FileName)
                        : queryable.OrderBy(e => e.FileName);
                case "Number":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Number)
                        : queryable.OrderBy(e => e.Number);
                case "":
                case null:
                    return queryable.OrderBy(e => e.Number);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<BookPage> Filter(this IQueryable<BookPage> queryable, BookPageFilter? filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.FileName!.Contains(filter.Text));
            }
            if (filter?.BookId != null)
            {
                queryable = queryable.Where(e => e.BookId == filter.BookId);
            }
            if (!string.IsNullOrEmpty(filter?.FileName))
            {
                queryable = queryable.Where(e => e.FileName!.Contains(filter.FileName));
            }
            if (filter?.NumberFrom != null)
            {
                queryable = queryable.Where(e => e.Number >= filter.NumberFrom);
            }
            if (filter?.NumberTo != null)
            {
                queryable = queryable.Where(e => e.Number <= filter.NumberTo);
            }
            return queryable;
        }
    }
}
