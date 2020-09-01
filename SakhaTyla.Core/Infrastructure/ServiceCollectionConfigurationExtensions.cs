using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SakhaTyla.Core.Infrastructure
{
    public static class ServiceCollectionConfigurationExtensions
    {
        public static IServiceCollection AddFromConfiguration(this IServiceCollection services, IConfiguration configuration, System.Reflection.Assembly[] assemblies)
        {
            var config = configuration.GetSection("Services");

            var types = assemblies.SelectMany(a => a.GetTypes()).ToList();
            foreach (var serviceSection in config.GetChildren())
            {
                var serviceType = types.FirstOrDefault(t => t.FullName == serviceSection.Key);
                var components = new List<string>();
                if (!string.IsNullOrEmpty(serviceSection.Value))
                {
                    components.Add(serviceSection.Value);
                }
                else
                {
                    foreach (var componentSection in serviceSection.GetChildren())
                    {
                        components.Add(componentSection.Value);
                    }
                }
                foreach (var component in components)
                {
                    var componentType = types.FirstOrDefault(t => t.FullName == component);
                    services.AddTransient(serviceType, componentType);
                }
            }

            return services;
        }
    }
}
