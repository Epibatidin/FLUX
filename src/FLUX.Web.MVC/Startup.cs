using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FLUX.Configuration.DependencyInjection;
using FLUX.Configuration.Config;

namespace FLUX.Web.MVC
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            string configFolder = @"D:\Develop\FLUX\src\FLUX.Configuration\Files\";


            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile(configFolder + "VirtualFileProvier.json")
                .AddJsonFile(configFolder + "Layer.json");

            Configuration = builder.Build();
            
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // add strongly typed configuration
            //services.AddOptions();

            // Add MVC dependencies
            services.AddMvc();

            var di = new DependencyInstaller();
            di.Install(services, Configuration);

            var sectionGrp = new VirtualFileAccessorSectionGroupReader();
            sectionGrp.Startup(services, Configuration);

            services.AddCaching();

            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromMinutes(30);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler();

            app.UseSession();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
