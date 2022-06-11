using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Messaging;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SakhaTyla.Core.Email;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Security;
using SakhaTyla.Infrastructure.Email;
using SakhaTyla.Migration.Infrastructure;
using SakhaTyla.Migration.Migrations;
using SakhaTyla.Migration.SourceDatabase;

namespace SakhaTyla.Migration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMigration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserInfoProvider, UserInfoProvider>();
            services.AddSingleton<IHostedService, MigrationWorker>();
            var assemblies = CoreHelper.GetPlatformAndAppAssemblies();
            services.AddSingleton<IMapper>(sp => new MapperConfiguration(cfg => { cfg.AddMaps(assemblies); }).CreateMapper());
            services.AddFromConfiguration(configuration, assemblies);

            services.AddScoped<DataMigration>();
            services.AddScoped<PageMigration>();
            services.AddScoped<WidgetMigration>();
            services.AddScoped<CategoryMigration>();
            services.AddScoped<BookAuthorMigration>();
            services.AddScoped<SourceLoader>();

            services.AddTransient<IEmailSender, EmailSender>();

            services.AddCynosuraMessaging(configuration, x => {
                x.AddInMemoryBus((context, sbc) =>
                {
                    sbc.ConfigureEndpoints(context);
                });
                x.AddConsumers(assemblies);
            });

            return services;
        }
    }
}
