namespace Zelf.SocialNetwork.Api.EntityFramework.Entity
{
	public class UserSubscription
	{
		public long UserId { get; set; }
		public User User { get; set; }

		public long SubscriptionId { get; set; }
		public User Subscription { get; set; }
	}
}