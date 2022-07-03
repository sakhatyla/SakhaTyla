using AutoMapper;
using Cynosura.Messaging;
using Cynosura.Web.Infrastructure;
using MassTransit;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Security;
using SakhaTyla.Web.Common.Infrastructure;
using SakhaTyla.Web.Front.Infrastructure;

namespace SakhaTyla.Web.Front
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWeb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IExceptionHandler, ValidationExceptionHandler>();
            services.AddScoped<IUserInfoProvider, UserInfoProvider>();
            var assemblies = CoreHelper.GetPlatformAndAppAssemblies();
            services.AddSingleton<IMapper>(sp => new MapperConfiguration(cfg => { cfg.AddMaps(assemblies); }).CreateMapper());
            services.AddFromConfiguration(configuration, assemblies);

            services.Configure<MassTransitServiceOptions>(configuration.GetSection("Messaging"));
            services.AddCynosuraMessaging(null, x =>
            {
                x.AddRabbitMqBus((context, sbc) =>
                {
                    sbc.ConfigureEndpoints(context);
                });
                x.AddConsumers(assemblies);
            });

            return services;
        }
    }
}
