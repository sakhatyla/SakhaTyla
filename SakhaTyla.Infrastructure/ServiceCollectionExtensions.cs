using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SakhaTyla.Core.ChatBots;
using SakhaTyla.Core.Email;
using SakhaTyla.Core.Formatters;
using SakhaTyla.Core.Search;
using SakhaTyla.Infrastructure.ChatBots;
using SakhaTyla.Infrastructure.Email;
using SakhaTyla.Infrastructure.Formatters;
using SakhaTyla.Infrastructure.Search;

namespace SakhaTyla.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<EmailSenderOptions>(configuration.GetSection("EmailSender"));
            services.AddTransient<IExcelFormatter, ExcelFormatter>();
            services.Configure<LuceneOptions>(configuration.GetSection("Lucene"));
            services.AddSingleton<LuceneContext>();
            services.AddSingleton<ISearchIndexWriter, LuceneIndexWriter>();
            services.AddTransient<ISearchIndexReader, LuceneIndexReader>();
            services.AddTransient<IChatBotMessageSender, TelegramMessageSender>();
            services.AddScoped<TelegramUpdateHandler>();
            services.AddScoped<TelegramReceiverService>();
            services.Configure<TelegramSettings>(configuration.GetSection("Telegram"));

            return services;
        }
    }
}
