using System.ComponentModel.DataAnnotations;

namespace Zelf.SocialNetwork.Api.WebApi.Models.AddSubscription
{
	public class AddSubscriptionRequest
	{
		[Required]
		public long UserId { get; set; }

		[Required]
		public long SubscriptionId { get; set; }
	}
}