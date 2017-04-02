using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Vega.Data;
using Vega.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Vega.Data.Abstract;
using Vega.Data.Repositories;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Reflection;

namespace WebApplicationBasic
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc().AddJsonOptions(opts =>
            {
                // Force Camel Case to JSON
                opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            }); 
            services.AddDbContext<VegaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Vega")));


            services.AddScoped<IMakeRepository, MakeRepository>();
            services.AddScoped<IModelRepository, ModelRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseExceptionHandler(
           builder =>
           {
               builder.Run(
                 async context =>
                 {
                     context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                     context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                     var error = context.Features.Get<IExceptionHandlerFeature>();
                     if (error != null)
                     {
                         context.Response.AddApplicationError(error.Error.Message);
                         await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                     }
                 });
           });


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

                VegaDbInitializer.Initialize(app.ApplicationServices);
        }
    }
}
