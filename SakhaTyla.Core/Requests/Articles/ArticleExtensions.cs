using System;
using System.Linq;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Articles.Models;

namespace SakhaTyla.Core.Requests.Articles
{
    public static class ArticleExtensions
    {
        public static IOrderedQueryable<Article> OrderBy(this IQueryable<Article> queryable, string propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "Title":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Title)
                        : queryable.OrderBy(e => e.Title);
                case "Text":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Text)
                        : queryable.OrderBy(e => e.Text);
                case "TextSource":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.TextSource)
                        : queryable.OrderBy(e => e.TextSource);
                case "FromLanguage":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.FromLanguage)
                        : queryable.OrderBy(e => e.FromLanguage);
                case "ToLanguage":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.ToLanguage)
                        : queryable.OrderBy(e => e.ToLanguage);
                case "Fuzzy":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Fuzzy)
                        : queryable.OrderBy(e => e.Fuzzy);
                case "Category":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Category)
                        : queryable.OrderBy(e => e.Category);
                case "":
                case null:
                    return queryable.OrderBy(e => e.Id);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<Article> Filter(this IQueryable<Article> queryable, ArticleFilter filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.Title.Contains(filter.Text) || e.Text.Contains(filter.Text) || e.TextSource.Contains(filter.Text));
            }
            if (!string.IsNullOrEmpty(filter?.Title))
            {
                queryable = queryable.Where(e => e.Title.Contains(filter.Title));
            }
            if (filter?.FromLanguageId != null)
            {
                queryable = queryable.Where(e => e.FromLanguageId == filter.FromLanguageId);
            }
            if (filter?.ToLanguageId != null)
            {
                queryable = queryable.Where(e => e.ToLanguageId == filter.ToLanguageId);
            }
            if (filter?.Fuzzy != null)
            {
                queryable = queryable.Where(e => e.Fuzzy == filter.Fuzzy);
            }
            if (filter?.CategoryId != null)
            {
                queryable = queryable.Where(e => e.CategoryId == filter.CategoryId);
            }
            return queryable;
        }
    }
}
