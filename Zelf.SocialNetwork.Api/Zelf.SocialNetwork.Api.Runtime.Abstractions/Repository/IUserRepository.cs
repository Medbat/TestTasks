using System.Threading;
using System.Threading.Tasks;

namespace Zelf.SocialNetwork.Api.Runtime.Abstractions.Repository
{
	public interface IUserRepository
	{
		Task CreateUserAsync(string userName, CancellationToken cancellationToken);

		Task AddUserSubscriptionAsync(
			long userId, 
			long subscriptionId, 
			CancellationToken cancellationToken);

		Task<long[]> GetMostPopularUsers(
			int? topCount,
			CancellationToken cancellationToken);
	}
}