using System.Threading;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Zelf.SocialNetwork.Api.Runtime.Abstractions.Repository;
using Zelf.SocialNetwork.Api.WebApi.Models.CreateUser;
using Zelf.SocialNetwork.Api.WebApi.Options;

namespace Zelf.SocialNetwork.Api.WebApi.Controllers
{
	[Route("api/users/create")]
	public class CreateUserController : ControllerBase
	{
		private readonly IUserRepository _userRepository;
		private readonly ControllersOptions _controllersOptions;

		public CreateUserController(
			IUserRepository userRepository,
			IOptions<ControllersOptions> controllersOptions)
		{
			_userRepository = userRepository;
			_controllersOptions = controllersOptions.Value;
		}

		[HttpPost]
		public async Task CreateUser([FromBody] CreateUserRequest request)
		{
			using var cts = new CancellationTokenSource(_controllersOptions.CreateUserTimeout);
			await _userRepository.CreateUserAsync(request.UserName, cts.Token);
		}
	}
}
