using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using Microsoft.EntityFrameworkCore;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.MenuItems.Models;

namespace SakhaTyla.Core.Requests.MenuItems
{
    public static class MenuItemExtensions
    {
        public static IOrderedQueryable<MenuItem> OrderBy(this IQueryable<MenuItem> queryable, string? propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "Menu":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Menu.Name)
                        : queryable.OrderBy(e => e.Menu.Name);
                case "Name":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Name)
                        : queryable.OrderBy(e => e.Name);
                case "Url":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Url)
                        : queryable.OrderBy(e => e.Url);
                case "Weight":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Weight)
                        : queryable.OrderBy(e => e.Weight);
                case "Parent":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Parent!.Name)
                        : queryable.OrderBy(e => e.Parent!.Name);
                case "":
                case null:
                    return queryable.OrderBy(e => e.TreeOrder)
                        .ThenBy(e => e.Id);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<MenuItem> Filter(this IQueryable<MenuItem> queryable, MenuItemFilter? filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.Name!.Contains(filter.Text) || e.Url!.Contains(filter.Text) || e.TreePath!.Contains(filter.Text) || e.TreeOrder!.Contains(filter.Text));
            }
            if (filter?.MenuId != null)
            {
                queryable = queryable.Where(e => e.MenuId == filter.MenuId);
            }
            if (!string.IsNullOrEmpty(filter?.Name))
            {
                queryable = queryable.Where(e => e.Name!.Contains(filter.Name));
            }
            if (!string.IsNullOrEmpty(filter?.Url))
            {
                queryable = queryable.Where(e => e.Url!.Contains(filter.Url));
            }
            if (filter?.WeightFrom != null)
            {
                queryable = queryable.Where(e => e.Weight >= filter.WeightFrom);
            }
            if (filter?.WeightTo != null)
            {
                queryable = queryable.Where(e => e.Weight <= filter.WeightTo);
            }
            if (filter?.ParentId != null)
            {
                queryable = queryable.Where(e => e.ParentId == filter.ParentId);
            }
            return queryable;
        }

        public async static Task CheckTreeLoop(this IEntityRepository<MenuItem> menuItemRepository, MenuItem menuItem)
        {
            if (menuItem.ParentId == null)
                return;
            if (menuItem.ParentId == menuItem.Id)
                throw new ServiceException($"Нельзя указать самого себя в качестве родителя");
            var parent = await menuItemRepository.GetEntities()
                .Where(e => e.Id == menuItem.ParentId)
                .FirstAsync();
            if (parent.TreePath!.Contains("/" + menuItem.Id + "/"))
                throw new ServiceException($"Нельзя указать наследника в качестве родителя");
        }

        public async static Task<IEntityRepository<MenuItem>> CalculateTree(this IEntityRepository<MenuItem> menuItemRepository, MenuItem menuItem)
        {
            IList<MenuItem> menuItems;
            string path;
            string order;
            if (menuItem.ParentId == null)
            {
                menuItems = await menuItemRepository.GetEntities()
                    .Where(e => e.MenuId == menuItem.MenuId && e.ParentId == null)
                    .ToListAsync();
                path = "/";
                order = "";
            }
            else
            {
                var parent = await menuItemRepository.GetEntities()
                    .Include(e => e.Children)
                    .Where(e => e.Id == menuItem.ParentId)
                    .FirstAsync();
                menuItems = parent.Children.ToList();
                path = parent.TreePath + parent.Id + "/";
                order = parent.TreeOrder!;

            }
            await CalculateTree(menuItemRepository, menuItems, path, order);
            return menuItemRepository;
        }

        private async static Task CalculateTree(IEntityRepository<MenuItem> menuItemRepository, IEnumerable<MenuItem> menuItems, string path, string order)
        {
            var sortedMenuItems = menuItems.OrderByDescending(t => t.Weight)
                .ThenBy(t => t.Id)
                .ToList();
            var format = CommonHelper.MultiplyString("0", CommonHelper.GetDigitCount(sortedMenuItems.Count));
            var i = 1;
            foreach (var page in sortedMenuItems)
            {
                page.TreeOrder = order + "/" + i.ToString(format);
                page.TreePath = path;
                var children = await menuItemRepository.GetEntities()
                    .Where(e => e.ParentId == page.Id)
                    .ToListAsync();
                await CalculateTree(menuItemRepository, children, page.TreePath + page.Id + "/", page.TreeOrder);
                i++;
            }
        }
    }
}
