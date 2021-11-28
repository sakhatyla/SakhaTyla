using System;
using System.Linq;
using Cynosura.Core.Services;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Files.Models;

namespace SakhaTyla.Core.Requests.Files
{
    public static class FileExtensions
    {
        public static IOrderedQueryable<File> OrderBy(this IQueryable<File> queryable, string propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "Name":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Name)
                        : queryable.OrderBy(e => e.Name);
                case "ContentType":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.ContentType)
                        : queryable.OrderBy(e => e.ContentType);
                case "Content":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Content)
                        : queryable.OrderBy(e => e.Content);
                case "Url":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Url)
                        : queryable.OrderBy(e => e.Url);
                case "Group":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Group)
                        : queryable.OrderBy(e => e.Group);
                case "":
                case null:
                    return queryable.OrderBy(e => e.Id);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<File> Filter(this IQueryable<File> queryable, FileFilter filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.Name.Contains(filter.Text) || e.ContentType.Contains(filter.Text) || e.Url.Contains(filter.Text));
            }
            if (!string.IsNullOrEmpty(filter?.Name))
            {
                queryable = queryable.Where(e => e.Name.Contains(filter.Name));
            }
            if (!string.IsNullOrEmpty(filter?.ContentType))
            {
                queryable = queryable.Where(e => e.ContentType.Contains(filter.ContentType));
            }

            if (!string.IsNullOrEmpty(filter?.Url))
            {
                queryable = queryable.Where(e => e.Url.Contains(filter.Url));
            }
            if (filter?.GroupId != null)
            {
                queryable = queryable.Where(e => e.GroupId == filter.GroupId);
            }
            return queryable;
        }

        public static void Validate(this string accept, string filename, string contentType)
        {
            if (string.IsNullOrEmpty(accept))
                return;
            var valid = accept.Split(",")
                .Select(a => a.Trim())
                .Any(a => filename.EndsWith(a) || MatchContentType(contentType, a));
            if (!valid)
            {
                throw new ServiceException("Invalid file format");
            }
        }

        private static bool MatchContentType(string contentType, string pattern)
        {
            if (pattern == null)
                return false;
            if (contentType == pattern)
                return true;

            var patternParts = pattern.Split("/");
            if (patternParts.Length == 2 && patternParts[1] == "*" && contentType.StartsWith(patternParts[0] + "/"))
            {
                return true;
            }
            return false;
        }

        public static byte[] ConvertToBytes(this System.IO.Stream input)
        {
            input.Position = 0;
            using var ms = new System.IO.MemoryStream();
            input.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
