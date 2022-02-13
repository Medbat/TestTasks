using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Zelf.SocialNetwork.Api.Runtime.Abstractions.Repository;
using Zelf.SocialNetwork.Api.WebApi.Models.AddSubscription;
using Zelf.SocialNetwork.Api.WebApi.Options;

namespace Zelf.SocialNetwork.Api.WebApi.Controllers
{
	[Route("api/users/addSub")]
	public class AddSubscriptionController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		private readonly ControllersOptions _controllersOptions;

		public AddSubscriptionController(
			IUserRepository userRepository,
			IOptions<ControllersOptions> controllersOptions)
		{
			_userRepository = userRepository;
			_controllersOptions = controllersOptions.Value;
		}

		[HttpPost]
		public async Task AddSubscription([FromBody] AddSubscriptionRequest request)
		{
			using var cts = new CancellationTokenSource(_controllersOptions.AddUserSubscriptionTimeout);
			await _userRepository.AddUserSubscriptionAsync(
				request.UserId,
				request.SubscriptionId,
				cts.Token);
		}
	}
}