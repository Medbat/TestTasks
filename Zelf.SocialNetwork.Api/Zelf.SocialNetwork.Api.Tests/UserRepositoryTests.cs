using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Zelf.SocialNetwork.Api.EntityFramework;
using Zelf.SocialNetwork.Api.Runtime.Repository;

namespace Zelf.SocialNetwork.Api.Tests
{
	public class UserRepositoryTests : IDisposable
	{
		private readonly DbConnection _connection;
		private readonly DbContextOptions<UserContext> _contextOptions;

		public UserRepositoryTests()
		{
			_connection = new SqliteConnection("Filename=:memory:");
			_connection.Open();

			_contextOptions = new DbContextOptionsBuilder<UserContext>()
				.UseSqlite(_connection)
				.Options;
				
			using var context = new UserContext(_contextOptions);

			context.Database.EnsureCreated();
			
			//context.SaveChanges();
		}

		// до конца не разобрался в исключении - что-то недонастроил, не хватило времени
        [Fact]
		public async Task AddUserTest()
		{
			using var context = new UserContext(_contextOptions);
			var repository = new UserRepository(context);

			await repository.CreateUserAsync("test1", CancellationToken.None);

			var result = await context.Users.SingleAsync(u => u.Name == "test1");
		}

		public void Dispose() => _connection.Dispose();
	}
}
