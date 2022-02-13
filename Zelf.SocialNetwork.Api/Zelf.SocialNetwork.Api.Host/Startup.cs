using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using Zelf.SocialNetwork.Api.Host.Configuration;
using Zelf.SocialNetwork.Api.WebApi.DependencyInjection;

namespace Zelf.SocialNetwork.Api.Host
{
	public class Startup
    {
	    private readonly ServiceConfiguration _configuration;

        private ILogger _logger;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration.Get<ServiceConfiguration>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services

	            // WebApi
                .BuildSocialNetworkWebApi(
                      options =>
                      {
                          options.CreateUserTimeout = _configuration.WebApi.Controllers.CreateUserTimeout;
                          options.AddUserSubscriptionTimeout =
	                          _configuration.WebApi.Controllers.AddUserSubscriptionTimeout;
                          options.MostPopularUsersTimeout = _configuration.WebApi.Controllers.MostPopularUsersTimeout;
                      }
                  );
                  
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env, 
            ILogger<Startup> logger)
        {
            _logger = logger;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Unhandled web request
            app.Run(
                context =>
                {
                    logger.LogWarning("Unhandled request. {Method} {Path}.",
                        context.Request.Method,
                        context.Request.Path
                    );

                    context.Response.Clear();
                    context.Response.StatusCode = StatusCodes.Status404NotFound;

                    return Task.CompletedTask;
                }
             );
        }
    }
}
