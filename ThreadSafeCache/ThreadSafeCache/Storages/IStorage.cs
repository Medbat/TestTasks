using System.Collections.Generic;

namespace ThreadSafeCache.Storages
{
	public interface IStorage<T> : IEnumerable<T>
	{
		int Add(T element);

		T Get(int index);

		void Remove(int index);

		int Length { get; }
	}
}