using System;
using System.Linq;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.BookLabels.Models;

namespace SakhaTyla.Core.Requests.BookLabels
{
    public static class BookLabelExtensions
    {
        public static IOrderedQueryable<BookLabel> OrderBy(this IQueryable<BookLabel> queryable, string? propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "Book":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Book.Name)
                        : queryable.OrderBy(e => e.Book.Name);
                case "Name":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Name)
                        : queryable.OrderBy(e => e.Name);
                case "Page":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Page.FileName)
                        : queryable.OrderBy(e => e.Page.FileName);
                case "":
                case null:
                    return queryable.OrderBy(e => e.Page.Number);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<BookLabel> Filter(this IQueryable<BookLabel> queryable, BookLabelFilter? filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.Name!.Contains(filter.Text));
            }
            if (filter?.BookId != null)
            {
                queryable = queryable.Where(e => e.BookId == filter.BookId);
            }
            if (!string.IsNullOrEmpty(filter?.Name))
            {
                queryable = queryable.Where(e => e.Name!.Contains(filter.Name));
            }
            if (filter?.PageId != null)
            {
                queryable = queryable.Where(e => e.PageId == filter.PageId);
            }
            return queryable;
        }
    }
}
