using Microsoft.EntityFrameworkCore;
using Zelf.SocialNetwork.Api.EntityFramework.Entity;

namespace Zelf.SocialNetwork.Api.EntityFramework
{
	public class UserContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public DbSet<UserSubscription> UserSubscriptions { get; set; }

		public UserContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
		{
			//Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserSubscription>()
				.HasKey(us => new { us.UserId, us.SubscriptionId });

			modelBuilder.Entity<UserSubscription>()
				.HasOne<User>(s => s.User)
				.WithMany(u => u.Subscriptions)
				.HasForeignKey(s => s.SubscriptionId);

			modelBuilder.Entity<UserSubscription>()
				.HasOne(s => s.User)
				.WithMany(u => u.Subscriptions)
				.HasForeignKey(s => s.UserId);
		}
	}
}
