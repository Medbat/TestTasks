using System.Reflection;
using CompanyName.Captcha.Api.Host.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var pathToExe = Assembly.GetExecutingAssembly().Location;
var pathToContentRoot = Path.GetDirectoryName(pathToExe);

Host
    .CreateDefaultBuilder(args)
    .UseCaptchaApiHost()
    .UseConsoleLifetime()
    .UseContentRoot(pathToContentRoot)
    .ConfigureAppConfiguration(
        builder => builder.SetBasePath(pathToContentRoot)
    )


    .Build()
    .Run();