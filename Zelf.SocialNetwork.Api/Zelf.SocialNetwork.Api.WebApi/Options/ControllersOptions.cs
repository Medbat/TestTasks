using System;

namespace Zelf.SocialNetwork.Api.WebApi.Options
{
	public class ControllersOptions
	{
		public TimeSpan CreateUserTimeout { get; set; }

		public TimeSpan AddUserSubscriptionTimeout { get; set; }

		public TimeSpan MostPopularUsersTimeout { get; set; }
	}
}
