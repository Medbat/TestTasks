using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zelf.SocialNetwork.Api.EntityFramework;
using Zelf.SocialNetwork.Api.EntityFramework.Entity;
using Zelf.SocialNetwork.Api.Runtime.Abstractions.Repository;

namespace Zelf.SocialNetwork.Api.Runtime.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly UserContext _userContext;

		public UserRepository(UserContext userContext)
		{
			_userContext = userContext;
		}

		public async Task CreateUserAsync(string userName, CancellationToken cancellationToken)
		{
			userName = userName.Trim();
			if (string.IsNullOrWhiteSpace(userName))
			{
				throw new ArgumentException("Name cannot be empty");
			}

			var user = new User
			{
				Name = userName
			};

			await _userContext.Users.AddAsync(user, cancellationToken);
			await _userContext.SaveChangesAsync(cancellationToken);
		}

		public async Task AddUserSubscriptionAsync(
			long userId, 
			long subscriptionId, 
			CancellationToken cancellationToken)
		{
			var user = await _userContext.Users.FindAsync(userId);
			var subscriptionUser = await _userContext.Users.FindAsync(subscriptionId);
			if (user != null 
			    && subscriptionUser != null
			    && (await _userContext.UserSubscriptions.FindAsync(user.Id, subscriptionUser.Id)) == null)
			{
				user.Subscriptions.Add(new UserSubscription
				{
					UserId = userId, SubscriptionId = subscriptionId
				});
				await _userContext.SaveChangesAsync(cancellationToken);
			}
		}

		public Task<long[]> GetMostPopularUsers(
			int? topCount,
			CancellationToken cancellationToken)
		{
			var b = _userContext.UserSubscriptions
				.AsNoTracking()
				.GroupBy(us => us.SubscriptionId)
				.Select(g => new { UserId = g.Key, Count = g.Count() })
				.OrderByDescending(g => g.Count);

			return topCount.HasValue 
				? Task.FromResult(b.Take(topCount.Value).Select(g => g.UserId).ToArray()) 
				: Task.FromResult(b.Select(g => g.UserId).ToArray());
		}
	}
}
