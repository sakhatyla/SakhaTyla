using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SakhaTyla.Core.Email;
using SakhaTyla.Core.Formatters;
using SakhaTyla.Infrastructure.Email;
using SakhaTyla.Infrastructure.Formatters;

namespace SakhaTyla.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<EmailSenderOptions>(configuration.GetSection("EmailSender"));
            services.AddTransient<IExcelFormatter, ExcelFormatter>();
            return services;
        }
    }
}
