using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Zelf.SocialNetwork.Api.Runtime.Abstractions.Repository;
using Zelf.SocialNetwork.Api.WebApi.Models.AddSubscription;
using Zelf.SocialNetwork.Api.WebApi.Options;

namespace Zelf.SocialNetwork.Api.WebApi.Controllers
{
	[Route("api/users/mostPopular")]
	public class MostPopularUsersController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		private readonly ControllersOptions _controllersOptions;

		public MostPopularUsersController(
			IUserRepository userRepository,
			IOptions<ControllersOptions> controllersOptions)
		{
			_userRepository = userRepository;
			_controllersOptions = controllersOptions.Value;
		}

		/// <summary>
		/// Возвращает идентификаторы топа популярных пользователей (с наибольшим количеством подписанных)
		/// </summary>
		[HttpGet]
		public async Task<IActionResult> GetMostPopularUsers(MostPopularUsersRequest request)
		{
			using var cts = new CancellationTokenSource(_controllersOptions.MostPopularUsersTimeout);
			return new JsonResult(await _userRepository.GetMostPopularUsers(
				request?.TopN,
				cts.Token));
		}
	}
}