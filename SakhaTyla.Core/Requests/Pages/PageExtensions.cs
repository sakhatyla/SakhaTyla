using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using Microsoft.EntityFrameworkCore;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Pages.Models;

namespace SakhaTyla.Core.Requests.Pages
{
    public static class PageExtensions
    {
        public static IOrderedQueryable<Page> OrderBy(this IQueryable<Page> queryable, string? propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "Type":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Type)
                        : queryable.OrderBy(e => e.Type);
                case "Name":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Name)
                        : queryable.OrderBy(e => e.Name);
                case "ShortName":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.ShortName)
                        : queryable.OrderBy(e => e.ShortName);
                case "Parent":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Parent!.Name)
                        : queryable.OrderBy(e => e.Parent!.Name);
                case "Header":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Header)
                        : queryable.OrderBy(e => e.Header);
                case "Body":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Body)
                        : queryable.OrderBy(e => e.Body);
                case "MetaTitle":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.MetaTitle)
                        : queryable.OrderBy(e => e.MetaTitle);
                case "MetaKeywords":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.MetaKeywords)
                        : queryable.OrderBy(e => e.MetaKeywords);
                case "MetaDescription":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.MetaDescription)
                        : queryable.OrderBy(e => e.MetaDescription);
                case "Image":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Image!.Name)
                        : queryable.OrderBy(e => e.Image!.Name);
                case "Preview":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Preview)
                        : queryable.OrderBy(e => e.Preview);
                case "ModificationDate":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.ModificationDate)
                        : queryable.OrderBy(e => e.ModificationDate);
                case "Route":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Route!.Path)
                        : queryable.OrderBy(e => e.Route!.Path);
                case "":
                case null:
                    return queryable.OrderBy(e => e.Id);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<Page> Filter(this IQueryable<Page> queryable, PageFilter? filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.Name!.Contains(filter.Text) || e.ShortName!.Contains(filter.Text) || e.Header!.Contains(filter.Text) || e.Body!.Contains(filter.Text) || e.MetaTitle!.Contains(filter.Text) || e.MetaKeywords!.Contains(filter.Text) || e.MetaDescription!.Contains(filter.Text) || e.TreePath!.Contains(filter.Text) || e.TreeOrder!.Contains(filter.Text) || e.Preview!.Contains(filter.Text));
            }
            if (filter?.Type != null)
            {
                queryable = queryable.Where(e => e.Type == filter.Type);
            }
            if (!string.IsNullOrEmpty(filter?.Name))
            {
                queryable = queryable.Where(e => e.Name!.Contains(filter.Name));
            }
            if (!string.IsNullOrEmpty(filter?.ShortName))
            {
                queryable = queryable.Where(e => e.ShortName!.Contains(filter.ShortName));
            }
            if (filter?.ParentId != null)
            {
                queryable = queryable.Where(e => e.ParentId == filter.ParentId);
            }
            if (!string.IsNullOrEmpty(filter?.Header))
            {
                queryable = queryable.Where(e => e.Header!.Contains(filter.Header));
            }
            if (!string.IsNullOrEmpty(filter?.Body))
            {
                queryable = queryable.Where(e => e.Body!.Contains(filter.Body));
            }
            if (!string.IsNullOrEmpty(filter?.MetaTitle))
            {
                queryable = queryable.Where(e => e.MetaTitle!.Contains(filter.MetaTitle));
            }
            if (!string.IsNullOrEmpty(filter?.MetaKeywords))
            {
                queryable = queryable.Where(e => e.MetaKeywords!.Contains(filter.MetaKeywords));
            }
            if (!string.IsNullOrEmpty(filter?.MetaDescription))
            {
                queryable = queryable.Where(e => e.MetaDescription!.Contains(filter.MetaDescription));
            }
            if (filter?.ImageId != null)
            {
                queryable = queryable.Where(e => e.ImageId == filter.ImageId);
            }
            if (!string.IsNullOrEmpty(filter?.Preview))
            {
                queryable = queryable.Where(e => e.Preview!.Contains(filter.Preview));
            }
            if (filter?.CommentContainerId != null)
            {
                queryable = queryable.Where(e => e.CommentContainerId == filter.CommentContainerId);
            }
            return queryable;
        }

        public async static Task CheckTreeLoop(this IEntityRepository<Page> pageRepository, Page page)
        {
            if (page.ParentId == null)
                return;
            if (page.ParentId == page.Id)
                throw new ServiceException($"Нельзя указать самого себя в качестве родителя");
            var parent = await pageRepository.GetEntities()
                .Where(e => e.Id == page.ParentId)
                .FirstAsync();
            if (parent.TreePath!.Contains("/" + page.Id + "/"))
                throw new ServiceException($"Нельзя указать наследника в качестве родителя");
        }

        public async static Task<IEntityRepository<Page>> CalculateTree(this IEntityRepository<Page> pageRepository, Page page)
        {
            IList<Page> pages = new List<Page>() { page };
            string path;
            if (page.ParentId == null)
            {
                path = "/";
            }
            else
            {
                var parent = page.Parent;
                if (parent == null)
                {
                    parent = await pageRepository.GetEntities()
                       .Where(e => e.Id == page.ParentId)
                       .FirstAsync();
                }
                path = parent.TreePath + parent.Id + "/";

            }
            await CalculateTree(pageRepository, pages, path);
            return pageRepository;
        }

        private async static Task CalculateTree(IEntityRepository<Page> pageRepository, IEnumerable<Page> pages, string path)
        {
            foreach (var page in pages)
            {
                var oldTreePath = page.TreePath;
                page.TreePath = path;
                if (oldTreePath != page.TreePath)
                {
                    var children = await pageRepository.GetEntities()
                        .Where(e => e.ParentId == page.Id)
                        .ToListAsync();
                    await CalculateTree(pageRepository, children, page.TreePath + page.Id + "/");
                }
            }
        }
    }
}
