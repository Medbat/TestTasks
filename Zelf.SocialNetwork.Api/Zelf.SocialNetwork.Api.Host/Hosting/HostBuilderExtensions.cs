using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zelf.SocialNetwork.Api.EntityFramework;
using Zelf.SocialNetwork.Api.Host.Configuration;

namespace Zelf.SocialNetwork.Api.Host.Hosting
{
	public static class HostBuilderExtensions
	{
		public static IHostBuilder UseZelfSocialNetworkApiHost(this IHostBuilder hostBuilder)
		{
			// конфигурация сервиса
			ServiceConfiguration configuration = null;

			hostBuilder
			
				// Infrastructure
				.ConfigureServices(
					(context, services) =>
					{
						configuration = context.Configuration.Get<ServiceConfiguration>();
						
						services

							// Database context pooled
							.AddDbContextPool<UserContext>(
								optionsBuilder =>
								{
									optionsBuilder.UseSqlite(configuration.Database.ConnectionString);
								},
								configuration.Database.PoolSize)

							.AddHostedService<CreateData>()

							.AddHealthChecks()
							.AddDbContextCheck<UserContext>()
						;
					}
				)

				//Kestrel Http
                .ConfigureWebHostDefaults(
                    webBuilder =>
                    {
                        webBuilder
                            .ConfigureServices(
                                (context, services) =>
                                {
                                    services
	                                    .Configure<KestrelServerOptions>(
		                                    context.Configuration.GetSection("Kestrel"))
										
	                                ;

                                }
                            )
                            .UseStartup<Startup>()
                            .UseKestrel()
						;
                    }
                )
				
				;


			return hostBuilder;
		}
	}
}
