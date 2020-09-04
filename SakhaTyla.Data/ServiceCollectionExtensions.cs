using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Cynosura.Core.Data;
using Cynosura.EF;
using SakhaTyla.Core.Entities;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
