using CompanyName.Captcha.Api.EntityFramework;
using CompanyName.Captcha.Api.Host.Configuration;
using CompanyName.Captcha.Api.WebApi.DependencyInjection;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Extensions.Hosting;
using NLog.Extensions.Logging;

namespace CompanyName.Captcha.Api.Host.Hosting;

public static class HostBuilderExtensions
{
    public static IHostBuilder UseCaptchaApiHost(this IHostBuilder hostBuilder)
    {
        ServiceConfiguration configuration = null;

        hostBuilder
            .UseNLog()
            .ConfigureServices(
                (context, services) =>
                {
                    configuration = context.Configuration.Get<ServiceConfiguration>();

                    // NLog configuration
                    LogManager.Setup().LoadConfigurationFromSection(context.Configuration);
                    LogManager.AutoShutdown = false;

                    services
                        .AddDbContextPool<CaptchaDbContext>(
                            optionsBuilder =>
                            {
                                optionsBuilder.UseSqlServer(
                                    new SqlConnectionStringBuilder(configuration.Database.ConnectionString)
                                    {
                                        UserID = configuration.Database.Username,
                                        Password = configuration.Database.Password
                                    }.ConnectionString
                                );
                            },
                            configuration.Database.PoolSize
                        );
                }
            )

            .ConfigureWebHostDefaults(
                webBuilder =>
                {
                    webBuilder
                        .ConfigureServices(
                            (context, services) =>
                            {
                                services.Configure<KestrelServerOptions>(context.Configuration.GetSection("Kestrel"));

                                services.BuildCaptchaApiWebApi();
                            }
                        )
                        .UseStartup<Startup>()
                        .UseKestrel()
                        ;
                })
            ;

        return hostBuilder;
    }
}