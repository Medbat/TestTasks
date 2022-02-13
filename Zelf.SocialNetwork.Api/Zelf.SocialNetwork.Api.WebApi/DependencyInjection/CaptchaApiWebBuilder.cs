using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Zelf.SocialNetwork.Api.Runtime.Abstractions.Repository;
using Zelf.SocialNetwork.Api.Runtime.Repository;
using Zelf.SocialNetwork.Api.WebApi.Options;

namespace Zelf.SocialNetwork.Api.WebApi.DependencyInjection
{
	public static class SocialNetworkApiWebBuilder
	{
		public static IServiceCollection BuildSocialNetworkWebApi(
			this IServiceCollection services,
			Action<ControllersOptions> controllersOptionsHandler)
		{
			services
			
				.AddScoped<IUserRepository, UserRepository>()

				.Configure(controllersOptionsHandler)
				.AddControllers()
				.AddApplicationPart(Assembly.GetExecutingAssembly())

				;

			return services;
		}
	}
}