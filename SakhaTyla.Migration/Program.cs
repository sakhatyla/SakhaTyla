using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using SakhaTyla.Core;
using SakhaTyla.Core.Entities;
using SakhaTyla.Data;
using SakhaTyla.Infrastructure;
using SakhaTyla.Migration.Infrastructure;

namespace SakhaTyla.Migration
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddSimpleConsole(c =>
                    {
                        c.TimestampFormat = "[yyyy-MM-dd HH:mm:ss] ";
                    });
                })
                .ConfigureServices((hostContext, services) =>
                {
                    Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

                    services.AddDbContext<DataContext>(options =>
                    {
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"));
                    });

                    services.AddIdentityCore<User>()
                        .AddRoles<Role>();

                    services.AddTransient(typeof(IStringLocalizer<>), typeof(DummyLocalizer<>));

                    services.AddOptions();

                    services.AddMigration(hostContext.Configuration);
                    services.AddInfrastructure(hostContext.Configuration);
                    services.AddData();
                    services.AddCore(hostContext.Configuration);
                });
        }
    }
}
