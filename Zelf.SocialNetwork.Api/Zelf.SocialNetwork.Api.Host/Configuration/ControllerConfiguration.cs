using System;

namespace Zelf.SocialNetwork.Api.Host.Configuration
{
	public class ControllerConfiguration
	{
		public TimeSpan CreateUserTimeout { get; set; }

		public TimeSpan AddUserSubscriptionTimeout { get; set; }

		public TimeSpan MostPopularUsersTimeout { get; set; }
	}
}