using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CMSApp.Data;
using CMSApp.Models;
using CMSApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CMSApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new RequireHttpsAttribute());
            });


            services.AddMvc();
            services.AddSingleton<IPageRepository, PageRepository>();
            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseIdentity();
            //1086769598044777
            //8b0d2fb44642ea7a50af01a13f7258ae
         app.UseFacebookAuthentication(new FacebookOptions()
            {
                AppId = "1086769598044777",
                AppSecret = "8b0d2fb44642ea7a50af01a13f7258ae"
            });
            //961944663385-q8m1m8fc5ml0d1bge4ip0tj3rr7u8iu1.apps.googleusercontent.com
            //drLhqKZLvFMwJyxiTQnDxbzS
           app.UseGoogleAuthentication(new GoogleOptions()
            {
               ClientId = "961944663385-q8m1m8fc5ml0d1bge4ip0tj3rr7u8iu1.apps.googleusercontent.com",
               ClientSecret = "drLhqKZLvFMwJyxiTQnDxbzS",
               CallbackPath= new PathString("/signin-google")


           });
            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Pages}/{action=Index}/{id?}");

                routes.MapRoute("custom", "CustomPage/{url}",
                          new { controller = "Pages", action = "CustomPage" },
                          new {  url = @"\S+" });

                routes.MapRoute(
         "Google API Sign-in",
         "signin-google",
         new { controller = "Account", action = "ExternalLoginCallbackRedirect" }
    );
            });



    }
    }
}
