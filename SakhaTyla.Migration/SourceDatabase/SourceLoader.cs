using System;
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
    Id,
    BookId,
    Name,
    PageId
from BookLabels
where IsDeleted = 0
order by BookId, Name");

            return bookLabels.ToList();
        }
    }
}
