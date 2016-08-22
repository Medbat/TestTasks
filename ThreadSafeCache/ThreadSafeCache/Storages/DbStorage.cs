using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EntityFramework.Extensions;
using Newtonsoft.Json;

namespace ThreadSafeCache.Storages
{
	public class DbStorage<T> : IStorage<T>
	{
		private readonly DbStorageContext _context;

		public DbStorage(DbStorageContext openedContext)
		{
			_context = openedContext;
		}

		public int Length => _context.Table.Count();

		public int Add(T element)
		{
			var added = _context.Table.Add(new DbStorageEntity {Value = JsonConvert.SerializeObject(element)});
			_context.SaveChanges();
			return added.ID;
		}

		public T Get(int index)
		{
			return JsonConvert.DeserializeObject<T>(_context.Table.Find(index).Value);
		}

		public void Remove(int index)
		{
			_context.Table.Where(e => e.ID == index).Delete();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return (IEnumerator<T>) _context.Table.Select(e => JsonConvert.DeserializeObject<T>(e.Value)).AsEnumerable();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}