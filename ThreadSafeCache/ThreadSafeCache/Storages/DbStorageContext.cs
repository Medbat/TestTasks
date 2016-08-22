using System.Data.Entity;

namespace ThreadSafeCache.Storages
{
	public class DbStorageContext : DbContext
	{
		public DbStorageContext() { }

		public DbStorageContext(string connectionString) : base(connectionString) { }

		public virtual DbSet<DbStorageEntity> Table { get; set; }
	}
}