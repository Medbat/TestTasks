using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Zelf.SocialNetwork.Api.Host.Hosting;

namespace Zelf.SocialNetwork.Api.Host.Console
{
	class Program
	{
		static Task Main(string[] args)
		{
			return CreateHostBuilder(args).Build().RunAsync();
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			var pathToExe = Assembly.GetExecutingAssembly().Location;
			var pathToContentRoot = Path.GetDirectoryName(pathToExe);

			return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
					.UseZelfSocialNetworkApiHost()
					.UseConsoleLifetime()
					.UseContentRoot(pathToContentRoot)
					.ConfigureAppConfiguration(
						builder => builder.SetBasePath(pathToContentRoot)
					)
				;
		}
	}
}