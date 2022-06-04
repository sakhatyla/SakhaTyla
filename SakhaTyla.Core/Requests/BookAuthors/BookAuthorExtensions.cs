using System;
using System.Linq;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.BookAuthors.Models;

namespace SakhaTyla.Core.Requests.BookAuthors
{
    public static class BookAuthorExtensions
    {
        public static IOrderedQueryable<BookAuthor> OrderBy(this IQueryable<BookAuthor> queryable, string? propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "LastName":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.LastName)
                        : queryable.OrderBy(e => e.LastName);
                case "FirstName":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.FirstName)
                        : queryable.OrderBy(e => e.FirstName);
                case "MiddleName":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.MiddleName)
                        : queryable.OrderBy(e => e.MiddleName);
                case "NickName":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.NickName)
                        : queryable.OrderBy(e => e.NickName);
                case "":
                case null:
                    return queryable.OrderBy(e => e.Id);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<BookAuthor> Filter(this IQueryable<BookAuthor> queryable, BookAuthorFilter? filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.LastName!.Contains(filter.Text) || e.FirstName!.Contains(filter.Text) || e.MiddleName!.Contains(filter.Text) || e.NickName!.Contains(filter.Text));
            }
            if (!string.IsNullOrEmpty(filter?.LastName))
            {
                queryable = queryable.Where(e => e.LastName!.Contains(filter.LastName));
            }
            if (!string.IsNullOrEmpty(filter?.FirstName))
            {
                queryable = queryable.Where(e => e.FirstName!.Contains(filter.FirstName));
            }
            if (!string.IsNullOrEmpty(filter?.MiddleName))
            {
                queryable = queryable.Where(e => e.MiddleName!.Contains(filter.MiddleName));
            }
            if (!string.IsNullOrEmpty(filter?.NickName))
            {
                queryable = queryable.Where(e => e.NickName!.Contains(filter.NickName));
            }
            return queryable;
        }
    }
}
