using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Zelf.SocialNetwork.Api.EntityFramework;
using Zelf.SocialNetwork.Api.EntityFramework.Entity;
using Zelf.SocialNetwork.Api.Runtime.Abstractions.Repository;

namespace Zelf.SocialNetwork.Api.Host
{
	/// <summary>
	/// Класс для заполнения БД тестовыми данными. ДРОПАЕТ БД ПРИ КАЖДОМ ЗАПУСКЕ
	/// </summary>
	public class CreateData : IHostedService
	{
		private readonly UserContext _userContext;
		private readonly IUserRepository _userRepository;

		public CreateData(UserContext userContext, IUserRepository userRepository)
		{
			_userContext = userContext;
			_userRepository = userRepository;
		}

		public async Task StartAsync(CancellationToken cancellationToken)
		{
			await _userContext.Database.EnsureDeletedAsync(cancellationToken);
			await _userContext.Database.EnsureCreatedAsync(cancellationToken);

			var user1 = new User { Name = "User1" };
			var user2 = new User { Name = "User2" };
			var user3 = new User { Name = "User3" };
			var user4 = new User { Name = "User4" };
            
			var sub1 = new UserSubscription { User = user1, Subscription = user2 };
			var sub2 = new UserSubscription { User = user1, Subscription = user3 };
			var sub3 = new UserSubscription { User = user4, Subscription = user1 };
			var sub4 = new UserSubscription { User = user4, Subscription = user2 };
			var sub5 = new UserSubscription { User = user4, Subscription = user3 };
			var sub6 = new UserSubscription { User = user2, Subscription = user3 };

			_userContext.AddRange(user1, user2, user3, 
				sub1, sub2, sub3, sub4, sub5, sub6);

			await _userContext.SaveChangesAsync(cancellationToken);

			//Console.WriteLine(
			//	string.Join(", ", 
			//		await _userRepository.GetMostPopularUsers(null, cancellationToken))
			//);
		}

		public Task StopAsync(CancellationToken cancellationToken)
		{
			return Task.CompletedTask;
		}
	}
}