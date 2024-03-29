﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SakhaTyla.Migration.SourceDatabase
{
    public class SourceLoader
    {
        private readonly IConfiguration _configuration;
        private readonly SqlConnection _connection;

        public SourceLoader(IConfiguration configuration)
        {
            _configuration = configuration;
            _connection = new SqlConnection(_configuration["ConnectionStrings:SourceConnection"]);
        }

        private async Task EnsureConnection()
        {
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                await _connection.OpenAsync();
            }
        }

        public async Task<List<SrcPage>> GetPagesAsync(bool partial)
        {
            await EnsureConnection();
            var pages = await _connection.QueryAsync<SrcPage>(@"select 
    Id,
    Title,
    Synonym,
    Contents,
    ContentsSource,
    Lang,
    IsPartial
from Pages p
where p.IsDeleted = 0 and p.IsPartial = @partial
order by p.Id", new { partial = partial });

            return pages.ToList();
        }

        public async Task<List<SrcBlog>> GetBlogsAsync()
        {
            await EnsureConnection();
            var blogs = await _connection.QueryAsync<SrcBlog>(@"select 
    Id,
    Title,
    Synonym
from Blogs
where IsDeleted = 0
order by Id");

            return blogs.ToList();
        }

        public async Task<List<SrcPost>> GetPostsAsync()
        {
            await EnsureConnection();
            var posts = await _connection.QueryAsync<SrcPost>(@"select 
    p.Id,
    p.Title,
    p.Synonym,
    p.Contents,
    p.ContentsSource,
    BlogId,
    p.Preview,
    p.DatePublished,
    b.Synonym as BlogSynonym
from Posts p
inner join Blogs b on p.BlogId = b.Id
where p.IsDeleted = 0
order by p.Id");

            return posts.ToList();
        }

        public async Task<List<SrcCategory>> GetCategoriesAsync()
        {
            await EnsureConnection();
            var categories = await _connection.QueryAsync<SrcCategory>(@"select 
    Id,
    Name
from Categories
order by Id");

            return categories.ToList();
        }

        public async Task<List<SrcBookAuthor>> GetBookAuthorsAsync()
        {
            await EnsureConnection();
            var bookAuthors = await _connection.QueryAsync<SrcBookAuthor>(@"select 
    Id,
    LastName,
    FirstName,
    MiddleName,
    NickName
from BookAuthors
order by Id");

            return bookAuthors.ToList();
        }

        public async Task<List<SrcBook>> GetBooksAsync()
        {
            await EnsureConnection();
            var books = await _connection.QueryAsync<SrcBook>(@"select 
    Id,
    Name,
    Synonym,
    IsHidden,
    Cover
from Books
where IsDeleted = 0 and Type = 1
order by Id");

            return books.ToList();
        }

        public async Task<List<SrcBookAuthorship>> GetBookAuthorshipsAsync()
        {
            await EnsureConnection();
            var bookAuthorships = await _connection.QueryAsync<SrcBookAuthorship>(@"select 
    BookId,
    AuthorId,
    [Order]
from BookAuthorships ba
inner join Books b on ba.BookId = b.Id and b.Type = 1
order by BookId, [Order]");

            return bookAuthorships.ToList();
        }

        public async Task<List<SrcBookPage>> GetBookPagesAsync()
        {
            await EnsureConnection();
            var bookPages = await _connection.QueryAsync<SrcBookPage>(@"select 
    Id,
    BookId,
    FileName,
    Number
from BookPages
where IsDeleted = 0
order by BookId, Number");

            return bookPages.ToList();
        }

        public async Task<List<SrcBookLabel>> GetBookLabelsAsync()
        {
            await EnsureConnection();
            var bookLabels = await _connection.QueryAsync<SrcBookLabel>(@"select 
    l.Id,
    l.BookId,
    l.Name,
    l.PageId
from BookLabels l
inner join BookPages p on p.Id = l.PageId
where l.IsDeleted = 0
order by l.BookId, p.Number");

            return bookLabels.ToList();
        }

        public async Task<List<SrcComment>> GetCommentsAsync()
        {
            await EnsureConnection();
            var comments = await _connection.QueryAsync<SrcComment>(@"select 
    c.Id,
    u.Email as UserEmail,
    c.Text,
    c.TextSource,
    c.DateCreated,
    c.DateModified,
    p.Synonym as PostSynonym,
    b.Synonym as BlogSynonym
from Comments c
inner join Posts p on c.CommentContainerId = p.CommentContainerId
inner join Blogs b on p.BlogId = b.Id
inner join AspNetUsers u on u.Id = c.UserId
where c.IsDeleted = 0 and c.Status = 1
order by c.Id");

            return comments.ToList();
        }

        public async Task<List<SrcUser>> GetUsersAsync()
        {
            await EnsureConnection();
            var users = await _connection.QueryAsync<SrcUser>(@"select 
    Id,
    Email,
    EmailConfirmed,
    Name,
    PasswordHash
from AspNetUsers u
where IsDeleted = 0 and
	(
		(select count(*) from Comments c where c.UserId=u.Id and c.IsDeleted = 0 and c.Status = 1) > 0 or
		(select count(*) from ArticleHistories ah where ah.UserCreatedId = u.Id) > 0 or
		(select count(*) from AspNetUserRoles ur where ur.UserId = u.Id) > 0
	)
order by Id");

            return users.ToList();
        }

        public async Task<List<SrcUserRole>> GetUserRolesAsync()
        {
            await EnsureConnection();
            var userRoles = await _connection.QueryAsync<SrcUserRole>(@"select 
    UserId,
    RoleId,
    r.Name as RoleName
from AspNetUserRoles ur
inner join AspNetRoles r on ur.RoleId = r.Id
order by UserId");

            return userRoles.ToList();
        }

        public async Task<List<SrcArticle>> GetArticlesAsync()
        {
            await EnsureConnection();
            var articles = await _connection.QueryAsync<SrcArticle>(@"select 
    a.Id,
    a.Title,
    a.Text,
    a.TextSource,
    a.FromLanguageId,
    lf.Name as FromLanguageName,
    a.ToLanguageId,
    lt.Name as ToLanguageName,
    a.IsDeleted,
    a.Fuzzy,
    a.CategoryId,
    c.Name as CategoryName,
    a.DateCreated,
    a.DateModified
from Articles a
inner join Languages lf on lf.Id = a.FromLanguageId
inner join Languages lt on lt.Id = a.ToLanguageId
left join Categories c on c.Id = a.CategoryId
order by a.Id");

            return articles.ToList();
        }

        public async Task<List<SrcArticleTag>> GetArticleTagsAsync()
        {
            await EnsureConnection();
            var articleTags = await _connection.QueryAsync<SrcArticleTag>(@"select 
    at.ArticleId,
    at.TagId,
    t.Name as TagName
from ArticleTags at
inner join Tags t on t.Id = at.TagId
where at.IsDeleted = 0
order by at.ArticleId");

            return articleTags.ToList();
        }

        public async Task<List<SrcArticleHistory>> GetArticleHistoriesAsync()
        {
            await EnsureConnection();
            var articleHistories = await _connection.QueryAsync<SrcArticleHistory>(@"select 
    ah.Id,
    ah.ArticleId,
    ah.NewTitle,
    ah.NewTextSource,
    u.Email as UserCreatedEmail,
    ah.DateCreated,
    ah.Type,
    ah.NewFuzzy
from ArticleHistories ah
inner join AspNetUsers u on u.Id = ah.UserCreatedId
order by ah.Id");

            return articleHistories.ToList();
        }
    }
}
