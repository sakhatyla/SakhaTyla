using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Cynosura.EF;

namespace SakhaTyla.Data
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private DataContext? _dataContext;

        public DatabaseFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public DbContext Get()
        {
            if (_dataContext == null)
            {
                _dataContext = _serviceProvider.GetRequiredService<DataContext>();
            }
            return _dataContext;
        }

        public void Dispose()
        {
            _dataContext?.Dispose();
        }
    }
}
