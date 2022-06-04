using System;
using System.Linq;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.BookAuthorships.Models;

namespace SakhaTyla.Core.Requests.BookAuthorships
{
    public static class BookAuthorshipExtensions
    {
        public static IOrderedQueryable<BookAuthorship> OrderBy(this IQueryable<BookAuthorship> queryable, string? propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "Book":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Book.Name)
                        : queryable.OrderBy(e => e.Book.Name);
                case "Author":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Author.LastName)
                        : queryable.OrderBy(e => e.Author.LastName);
                case "Weight":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Weight)
                        : queryable.OrderBy(e => e.Weight);
                case "":
                case null:
                    return queryable.OrderByDescending(e => e.Weight);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<BookAuthorship> Filter(this IQueryable<BookAuthorship> queryable, BookAuthorshipFilter? filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
            }
            if (filter?.BookId != null)
            {
                queryable = queryable.Where(e => e.BookId == filter.BookId);
            }
            if (filter?.AuthorId != null)
            {
                queryable = queryable.Where(e => e.AuthorId == filter.AuthorId);
            }
            if (filter?.WeightFrom != null)
            {
                queryable = queryable.Where(e => e.Weight >= filter.WeightFrom);
            }
            if (filter?.WeightTo != null)
            {
                queryable = queryable.Where(e => e.Weight <= filter.WeightTo);
            }
            return queryable;
        }
    }
}
