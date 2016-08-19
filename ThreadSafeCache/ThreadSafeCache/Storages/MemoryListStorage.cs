using System.Collections;
using System.Collections.Generic;

namespace ThreadSafeCache.Storages
{
	public class MemoryListStorage<T> : IStorage<T>
	{
		private readonly List<T> _elements = new List<T>();

		public void Remove(int index)
		{
			_elements.RemoveAt(index);
		}

		public int Length => _elements.Count;

		public void Add(T element)
		{
			_elements.Add(element);
		}

		public T Get(int index)
		{
			return _elements[index];
		}

		public IEnumerator<T> GetEnumerator()
		{
			return _elements.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}