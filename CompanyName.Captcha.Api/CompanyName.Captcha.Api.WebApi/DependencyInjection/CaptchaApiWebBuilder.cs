using System.Reflection;
using CompanyName.Captcha.Api.Primitives.UploadCaptcha.Options;
using CompanyName.Captcha.Api.Runtime;
using CompanyName.Captcha.Api.Runtime.Abstractions;
using CompanyName.Captcha.Api.Runtime.Abstractions.Repository;
using CompanyName.Captcha.Api.Runtime.Abstractions.UploadCaptcha;
using CompanyName.Captcha.Api.Runtime.UploadCaptcha.Handlers;
using Microsoft.Extensions.Options;

namespace CompanyName.Captcha.Api.WebApi.DependencyInjection;

public static class CaptchaApiWebBuilder
{
    public static IServiceCollection BuildCaptchaApiWebApi(this IServiceCollection services)
    {
        services

            .AddScoped<IMalwareDetector, DummyMalwareDetector>()
            .AddScoped<IArchiveExtractor, ZipArchiveExtractor>()
            // ...

            .AddScoped<IUploadCaptchaHandler>(provider =>
            {
                var h1 = new UploadCaptchaMalwareHandler(provider.GetService<DummyMalwareDetector>());
                var h2 = new UploadCaptchaExtractHandler(
                    provider.GetService<IArchiveExtractor>(),
                    provider.GetService<IOptions<UploadCaptchaExtractHandlerOptions>>());
                var h3 = new UploadCaptchaSaveDataHandler(provider.GetService<ICaptchaRepository>());
                h1.SetNext(h2);
                h2.SetNext(h3);
                return h1;
            })

            .AddControllers()
                
            .AddApplicationPart(Assembly.GetExecutingAssembly())

            ;

        return services;
    }
}