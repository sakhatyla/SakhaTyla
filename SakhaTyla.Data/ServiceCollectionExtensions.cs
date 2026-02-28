using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cynosura.Core.Data;
using Cynosura.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SakhaTyla.Core.Entities;
using SakhaTyla.Core.Infrastructure;

namespace SakhaTyla.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddData(this IServiceCollection services)
        {
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            services.AddScoped<IDatabaseFactory, DatabaseFactory>();
            services.AddScoped<IEntityRepository<Article>, ArticleRepository>();
            services.AddEntityRepositories<TrackedEntity>(typeof(TrackedEntityRepository<>));
            services.AddEntityRepositories<BaseEntity>(typeof(BaseEntityRepository<>));
            services.AddTransient<IUserStore<User>, CustomUserStore>();
            services.AddTransient<IRoleStore<Role>, CustomRoleStore>();
            services.AddScoped<IDataContext>(sp => sp.GetRequiredService<DataContext>());
            services.AddCynosuraEF();
            return services;
        }

        private static void AddEntityRepositories<T>(this IServiceCollection services, Type entityRepository)
        {
            var type = typeof(T);
            var entityTypes = type.Assembly
                .GetTypes()
                .Where(p => type.IsAssignableFrom(p) && p.IsClass)
                .ToList();
            foreach (var entityType in entityTypes)
            {
                var entityRepositoryType = entityRepository.MakeGenericType(entityType);
                var entityRepositoryInterface = typeof(IEntityRepository<>).MakeGenericType(entityType);
                services.TryAddScoped(entityRepositoryInterface, entityRepositoryType);
            }
        }
    }
}
