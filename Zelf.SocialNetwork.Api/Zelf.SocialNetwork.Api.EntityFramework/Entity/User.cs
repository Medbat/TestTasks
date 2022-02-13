using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zelf.SocialNetwork.Api.EntityFramework.Entity
{
	public class User
	{
		public long Id { get; set; }

		[Column(TypeName = "varchar(64)")]
		public string Name { get; set; }

		public ICollection<UserSubscription> Subscriptions { get; set; } = new List<UserSubscription>();
	}
}