using System;
using BeerApp.Bll.Beers;
using BeerApp.Entities;
using BeerApp.Service;
using BeerApp.Service.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BeerApp.Web
{
    public class Startup
    {
		/*
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
*/		
		// updated version of ctor without rudimental code
	    public Startup(IConfiguration configuration)
	    {
		    Configuration = configuration;
	    }
		
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
	        services.Configure<CookiePolicyOptions>(options =>
	        {
		        options.CheckConsentNeeded = context => true;
		        options.MinimumSameSitePolicy = SameSiteMode.None;
	        });

	        services.AddDistributedMemoryCache();

	        services.AddSession(options =>
	        {
		        // Set a short timeout for easy testing.
		        options.IdleTimeout = TimeSpan.FromSeconds(10);
		        options.Cookie.HttpOnly = true;
	        });


	        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);;

			// init API connection settings
	        services.Configure<BreweryDBSettings>(Configuration.GetSection(nameof(BreweryDBSettings)));
	        services.AddScoped(sp => sp.GetService<IOptionsSnapshot<BreweryDBSettings>>().Value);
	        services.AddOptions();

	        services.AddCors(opts =>
	        {
		        opts.AddPolicy("CorsPolicy",
			        builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
	        });

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
				
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
	            app.UseHsts();
            }

	        app.UseCors("CorsPolicy");
	        app.UseHttpsRedirection();
	        app.UseStaticFiles();
	        app.UseCookiePolicy();
	       
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
