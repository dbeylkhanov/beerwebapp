using BeerApp.Bll.Beers;
using BeerApp.Entities;
using BeerApp.Service;
using BeerApp.Service.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace BeerApp.Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
	        var builder = new ConfigurationBuilder()
		        .SetBasePath(env.ContentRootPath)
		        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
		        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
		        .AddEnvironmentVariables();

	        if (env.IsDevelopment())
	        {
		        // For more details see http://go.microsoft.com/fwlink/?LinkID=532709
		        builder.AddUserSecrets<Startup>();
	        }

	        Configuration = builder.Build();

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
	        services.AddMvc();

			// init API connection settings
	        services.Configure<BreweryDBSettings>(Configuration.GetSection(nameof(BreweryDBSettings)));

	        services.AddOptions();

	        // Register the Swagger generator, defining 1 or more Swagger documents
	        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "BreweryDbAPI", Version = "v1"}); });

	        services.AddScoped<IBeerManager, BeerManager>();
	        services.AddScoped<IBeerService, BeerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
	                HotModuleReplacementEndpoint = "/dist/__webpack_hmr"  
                });

	            //app.UseSwagger();

	            //app.UseSwaggerUI(c =>
	            //{
		           // c.SwaggerEndpoint("/swagger/v1/swagger.json", "BreweryDbAPI V1");
	            //});
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
	       
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
