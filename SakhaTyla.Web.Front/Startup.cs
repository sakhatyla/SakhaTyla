using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Cynosura.Web;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders;
using SakhaTyla.Core;
using SakhaTyla.Core.Entities;
using SakhaTyla.Data;
using SakhaTyla.Infrastructure;

namespace SakhaTyla.Web.Front
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentityCore<User>()
                .AddRoles<Role>();

            services.AddPortableObjectLocalization(options => options.ResourcesPath = "Localization");

            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });

            services.AddMvc()
                .AddMvcOptions(o =>
                {
                    o.Filters.Add(typeof(ExceptionLoggerFilter), 10);
                })
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
            services.AddRazorPages();

            services.AddHttpContextAccessor();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            services.AddWeb(Configuration);
            services.AddInfrastructure(Configuration);
            services.AddData();
            services.AddCore(Configuration);
            services.AddCynosuraWeb();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error{0}");
                app.UseForwardedHeaders();
                app.UseHsts();
            }

            var supportedCultures = new[]
            {
                new CultureInfo("ru-RU")
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("ru-RU"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapFallbackToPage("{*path}", "/Page");
            });
        }
    }
}
