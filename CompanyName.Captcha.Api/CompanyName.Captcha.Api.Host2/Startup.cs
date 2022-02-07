using CompanyName.Captcha.Api.Host.Configuration;

namespace CompanyName.Captcha.Api.Host;

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
        // TODO: Swagger
                
        // TODO: Healthchecks
        services.AddHealthChecks();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(
        IApplicationBuilder app, 
        ILogger<Startup> logger)
    {
        _logger = logger;

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        // Unhandled web request
        app.Run(
            context =>
            {
                logger.LogWarning("Unhandled request: {Method} {Path}",
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