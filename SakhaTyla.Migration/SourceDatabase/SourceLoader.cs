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
            var sections = await _connection.QueryAsync<SrcPage>(@"select 
    Title,
    Synonym,
    Contents,
    ContentsSource,
    Lang,
    IsPartial
from Pages p
where p.IsDeleted=0 and p.IsPartial=@partial
order by p.Id", new { partial = partial });

            return sections.ToList();
        }

        public async Task<List<SrcCategory>> GetCategoriesAsync()
        {
            await EnsureConnection();
            var sections = await _connection.QueryAsync<SrcCategory>(@"select 
    Id,
    Name
from Categories
order by Id");

            return sections.ToList();
        }
    }
}
