using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using Cynosura.Core.Data;
using Microsoft.EntityFrameworkCore;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Requests.Comments.Models;

namespace SakhaTyla.Core.Requests.Comments
{
    public static class CommentExtensions
    {
        public static IOrderedQueryable<Comment> OrderBy(this IQueryable<Comment> queryable, string? propertyName, OrderDirection? direction)
        {
            switch (propertyName)
            {                
                case "Author":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.Author!.UserName)
                        : queryable.OrderBy(e => e.Author!.UserName);
                case "CreationDate":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.CreationDate)
                        : queryable.OrderBy(e => e.CreationDate);
                case "TreeOrder":
                    return direction == OrderDirection.Descending
                        ? queryable.OrderByDescending(e => e.TreeOrder + ":")
                        : queryable.OrderBy(e => e.TreeOrder);
                case "":
                case null:
                    return queryable.OrderBy(e => e.TreeOrder);
                default:
                    throw new ArgumentException("Property not found", nameof(propertyName));
            }
        }

        public static IQueryable<Comment> Filter(this IQueryable<Comment> queryable, CommentFilter? filter)
        {
            if (!string.IsNullOrEmpty(filter?.Text))
            {
                queryable = queryable.Where(e => e.Text!.Contains(filter.Text) || e.TextSource!.Contains(filter.Text) || e.TreePath!.Contains(filter.Text) || e.TreeOrder!.Contains(filter.Text));
            }
            if (filter?.ContainerId != null)
            {
                queryable = queryable.Where(e => e.ContainerId == filter.ContainerId);
            }
            if (!string.IsNullOrEmpty(filter?.TextSource))
            {
                queryable = queryable.Where(e => e.TextSource!.Contains(filter.TextSource));
            }
            if (filter?.AuthorId != null)
            {
                queryable = queryable.Where(e => e.AuthorId == filter.AuthorId);
            }
            if (filter?.ParentId != null)
            {
                queryable = queryable.Where(e => e.ParentId == filter.ParentId);
            }
            return queryable;
        }

        public async static Task<IEntityRepository<Comment>> CalculateTree(this IEntityRepository<Comment> commentRepository, Comment comment)
        {
            IList<Comment> comments;
            string path;
            string order;
            if (comment.ParentId == null)
            {
                comments = await commentRepository.GetEntities()
                    .Where(e => e.ParentId == null && e.ContainerId == comment.ContainerId)
                    .ToListAsync();
                path = "/";
                order = "";
            }
            else
            {
                var parent = await commentRepository.GetEntities()
                    .Include(e => e.Children)
                    .Where(e => e.Id == comment.ParentId)
                    .FirstAsync();
                comments = parent.Children.ToList();
                path = parent.TreePath + parent.Id + "/";
                order = parent.TreeOrder!;

            }
            await CalculateTree(commentRepository, comments, path, order);
            return commentRepository;
        }

        private async static Task CalculateTree(IEntityRepository<Comment> commentRepository, IEnumerable<Comment> comments, string path, string order)
        {
            var sortedComments = comments.OrderByDescending(t => t.AuthorId != null ? 0 : 1)
                .ThenByDescending(t => t.Id)
                .ToList();
            var format = CommonHelper.MultiplyString("0", CommonHelper.GetDigitCount(sortedComments.Count));
            var i = 1;
            foreach (var comment in sortedComments)
            {
                comment.TreeOrder = order + "/" + i.ToString(format);
                comment.TreePath = path;
                var children = await commentRepository.GetEntities()
                    .Where(e => e.ParentId == comment.Id)
                    .ToListAsync();
                await CalculateTree(commentRepository, children, comment.TreePath + comment.Id + "/", comment.TreeOrder);
                i++;
            }
        }

        public static string ProcessCommentText(this string text)
        {
            text = HttpUtility.HtmlEncode(text);
            text = text.Replace("\r\n", "\n");
            text = text.Replace("\n", "<br/>");
            return text;
        }
    }
}
