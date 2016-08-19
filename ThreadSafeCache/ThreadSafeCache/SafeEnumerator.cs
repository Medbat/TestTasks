using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace ThreadSafeCache
{
	public class SafeEnumerator<T> : IEnumerator<T>
	{
		private readonly IEnumerator<T> _enumerator;
		private readonly object _lock;

		public T Current => _enumerator.Current;

		object IEnumerator.Current => Current;

		public SafeEnumerator(IEnumerator<T> inner, object @lock)
		{
			_enumerator = inner;
			_lock = @lock;
			Monitor.Enter(_lock);
		}

		public void Dispose()
		{
			Monitor.Exit(_lock);
		}

		public bool MoveNext()
		{
			return _enumerator.MoveNext();
		}

		public void Reset()
		{
			_enumerator.Reset();
		}
	}
}