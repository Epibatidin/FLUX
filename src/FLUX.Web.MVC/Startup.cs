using System;
using FLUX.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using FLUX.Configuration.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Razor;

namespace FLUX.Web.MVC
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            string configFolder = @"D:\Develop\FLUX\src\FLUX.Configuration\Files\";

            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile(configFolder + "VirtualFileProvier.json")
                .AddJsonFile(configFolder + "Layer.json");

            Configuration = builder.Build();
            
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add MVC dependencies            
            services.AddMvc();
            services.AddOptions();
            services.Configure<RazorViewEngineOptions>(o =>
            {
                o.ViewLocationFormats.Insert(1, "/Views/{0}.cshtml");
            });

            var fi = new FrameworkInstaller();
            fi.Install(services);

            var di = new DependencyInstaller();
            di.Install(services, Configuration);
            
            var sectionGrp = new ApplicationStarter();
            sectionGrp.Startup(services, Configuration);

            InstallSession(services);
        }

        private void InstallSession(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();

            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromMinutes(5);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                //app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseIISPlatformHandler();

            app.UseSession();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
