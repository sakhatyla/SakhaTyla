using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SakhaTyla.Core.FileStorage;
using SakhaTyla.Core.Indexers;
using SakhaTyla.Core.Infrastructure;
using SakhaTyla.Core.Messaging.WorkerRuns;
using SakhaTyla.Core.Workers;

namespace SakhaTyla.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IValidatorFactory, ServiceProviderValidatorFactory>();
            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<ServiceFactory>(sp =>
            {
                return t => sp.GetRequiredService(t);
            });
            services.AddAllRequestHandlers();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddAllValidators();
            services.Configure<FileSystemStorageSettings>(configuration.GetSection("FileSystemStorage"));
            services.AddTransient<IWorkerRunner, WorkerRunner>();
            services.AddTransient<TestWorker>();
            services.AddTransient<SearchIndexerWorker>();
            services.AddTransient<ArticleIndexer>();
            return services; 
        }

        private static void AddAllRequestHandlers(this IServiceCollection services)
        {
            var handlerTypes = new[] { typeof(IRequestHandler<>), typeof(IRequestHandler<,>) };
            var handlers = typeof(ServiceCollectionExtensions).Assembly
                .GetTypes()
                .Where(t => t.GetInterfaces().Any(i => handlerTypes.Any(ht => ht.Name == i.Name)))
                .ToList();

            foreach (var handler in handlers)
            {
                services.AddAsImplementedInterfaces(handler, ServiceLifetime.Transient);
            }
        }

        private static void AddAllValidators(this IServiceCollection services)
        {
            var validatorTypes = new[] { typeof(IValidator<>) };
            var validators = typeof(ServiceCollectionExtensions).Assembly
                .GetTypes()
                .Where(t => t.GetInterfaces().Any(i => validatorTypes.Any(ht => ht.Name == i.Name)))
                .ToList();

            foreach (var validator in validators)
            {
                services.AddAsImplementedInterfaces(validator, ServiceLifetime.Transient);
            }
        }

        private static void AddAsImplementedInterfaces(this IServiceCollection services, Type type, ServiceLifetime lifetime)
        {
            var interfaces = type.GetTypeInfo().ImplementedInterfaces
                .Where(i => i != typeof(IDisposable) && (i.IsPublic));

            foreach (var interfaceType in interfaces)
            {
                services.Add(new ServiceDescriptor(interfaceType, type, lifetime));
            }
        }
    }
}
