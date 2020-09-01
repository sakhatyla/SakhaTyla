using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Security;
using SakhaTyla.Worker.Infrastructure;

namespace SakhaTyla.Worker
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWorker(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserInfoProvider, UserInfoProvider>();
            services.AddSingleton<IHostedService, MainWorker>();
            services.AddTransient<CoreLogProvider>();
            var assemblies = CoreHelper.GetPlatformAndAppAssemblies();
            services.AddSingleton<IMapper>(sp => new MapperConfiguration(cfg => { cfg.AddMaps(assemblies); }).CreateMapper());
            services.AddFromConfiguration(configuration, assemblies);
            return services;
        }
    }
}
