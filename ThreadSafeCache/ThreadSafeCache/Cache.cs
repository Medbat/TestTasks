using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThreadSafeCache.Exceptions;
using ThreadSafeCache.Storages;

namespace ThreadSafeCache
{
	public class Cache<T> : IEnumerable<T>
	{
		private readonly object _readWriteLock = new object();
		private readonly IStorage<T> _storage;
		private readonly List<DateTime> _lifeTimes;

		private TimeSpan? _autoReleaseInterval;
		private Task _autoReleaser;
		private CancellationTokenSource _ctx = new CancellationTokenSource();

		public delegate void ObjectDeletedEventhandler(object sender, ObjectDeletedEventArgs args);
		public event ObjectDeletedEventhandler ObjectDeleted;

		public TimeSpan? LifeTime;

		public TimeSpan? AutoReleaseInterval
		{
			get { return _autoReleaseInterval; }
			set
			{
				_autoReleaseInterval = value;
				if (_autoReleaser != null && _autoReleaser.Status == TaskStatus.Running)
				{
					_ctx.Cancel();
					_autoReleaser.Wait();
				}
				_ctx = new CancellationTokenSource();
				_autoReleaser = new Task(AutoReleaseOld, _ctx.Token);
				_autoReleaser.Start();
			}
		}

		public int CacheSize => _storage.Length;

		public Cache(IStorage<T> storage, TimeSpan? lifeTime = null, TimeSpan? autoReleaseIntreval = null)
		{
			_storage = storage;
			LifeTime = lifeTime;
			AutoReleaseInterval = autoReleaseIntreval;
			_lifeTimes = new List<DateTime>();
		}

		public T Get(int index)
		{
			lock (_readWriteLock)
			{
				return _storage.Get(index);
			}
		}

		public T GetIfActual(int index)
		{
			if (!IsActual(_lifeTimes[index]))
			{
				DeleteElement(index);
				throw new ElementNotActualException($"Element {index} is not actual anymore");
			}
			lock (_readWriteLock)
			{
				return _storage.Get(index);
			}
		}

		public void Add(T element)
		{
			lock (_readWriteLock)
			{
				_storage.Add(element);
				_lifeTimes.Add(DateTime.Now);
			}
		}

		public void ReleaseOld()
		{
			lock (_readWriteLock)
			{
				for (var i = 0; i < _lifeTimes.Count; i++)
				{
					if (IsActual(_lifeTimes[i])) continue;
					DeleteElement(i);
					i--;
				}
			}
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new SafeEnumerator<T>(_storage.GetEnumerator(), _readWriteLock);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private void DeleteElement(int index)
		{
			lock (_readWriteLock)
			{
				_lifeTimes.RemoveAt(index);
				_storage.Remove(index);
				RaiseObjectDeletedEvent(new ObjectDeletedEventArgs { Index = index });
			}
		}

		private void AutoReleaseOld()
		{
			while (true)
			{
				if (!AutoReleaseInterval.HasValue || !LifeTime.HasValue)
					return;
				var canceled = _ctx.Token.WaitHandle.WaitOne(AutoReleaseInterval.Value);
				if (canceled)
					return;
				ReleaseOld();
			}
		}

		private bool IsActual(DateTime created)
		{
			if (!LifeTime.HasValue)
				return true;
			return created + LifeTime.Value >= DateTime.Now;
		}

		private void RaiseObjectDeletedEvent(ObjectDeletedEventArgs args)
		{
			ObjectDeleted?.Invoke(this, args);
		}
	}
}