using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using EntityFramework.Extensions;

namespace ThreadSafeCache.Storages
{
	public class DbStorage<T> : IStorage<T>
	{
		private readonly string _connectionString;

		public DbStorage(string connectionString)
		{
			_connectionString = connectionString;
		}

		public int Length
		{
			get
			{
				using (var connection = new Db(_connectionString))
				{
					return connection.Table.Count();
				}
			}
		}

		public void Add(T element)
		{
			using (var connection = new Db(_connectionString))
			{
				connection.Table.Add(new Entity {Value = element});
			}
		}

		public T Get(int index)
		{
			using (var connection = new Db(_connectionString))
			{
				return connection.Table.First(e => e.ID == index).Value;
			}
		}

		public void Remove(int index)
		{
			using (var connection = new Db(_connectionString))
			{
				connection.Table.Where(e => e.ID == index).Delete();
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			using (var connection = new Db(_connectionString))
			{
				return (IEnumerator<T>)connection.Table.Local.GetEnumerator();
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private class Db : DbContext
		{
			public Db(string connectionString) : base(connectionString)
			{
			}

			public DbSet<Entity> Table { get; set; }
		}

		private class Entity
		{
			[Key]
			public int ID;

			public T Value { get; set; }
		}
	}
}