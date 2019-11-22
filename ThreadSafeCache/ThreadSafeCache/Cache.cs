using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ThreadSafeCache.Exceptions;
using ThreadSafeCache.Storages;

namespace ThreadSafeCache
{
	/// <summary>
	/// Класс для кэширования данных
	/// </summary>
	/// <typeparam name="T">Тип кэшируемых данных</typeparam>
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

		/// <summary>
		/// Получает или задаёт интервал для автоматического удаления элементов с истёкшим сроком хранения. При задании нового значения первый отсчёт начинается заново
		/// </summary>
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

		/// <summary>
		/// Количество закэшированных устаревших/неустаревших на данный момент элементов
		/// </summary>
		public int CacheSize => _storage.Length;

		/// <summary>
		/// Конструктор для создания экземпляра кэша
		/// </summary>
		/// <param name="storage">Хранилище данных</param>
		/// <param name="lifeTime">Время жизни элементов</param>
		/// <param name="autoReleaseIntreval">Интервал автоматической очистки просроченных элементов</param>
		public Cache(IStorage<T> storage, TimeSpan? lifeTime = null, TimeSpan? autoReleaseIntreval = null)
		{
			_storage = storage;
			LifeTime = lifeTime;
			AutoReleaseInterval = autoReleaseIntreval;
			_lifeTimes = new List<DateTime>();
		}

		/// <summary>
		/// Получает кэшированный объект по заданному индексу. Проверки на актуальность элемента не происходит
		/// </summary>
		/// <param name="index">Индекс запрашиваемого объекта</param>
		/// <returns>Кэшированный объект</returns>
		public T Get(int index)
		{
			lock (_readWriteLock)
			{
				return _storage.Get(index);
			}
		}

		/// <summary>
		/// Пытается получить кэшированный объект по заданному индексу. Если он устарел, выбрасывается ElementNotActualException
		/// </summary>
		/// <param name="index">Индекс запрашиваемого объекта</param>
		/// <exception cref="ElementNotActualException"></exception>
		/// <returns>Кэшированный объект</returns>
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

		/// <summary>
		/// Добавляет элемент в кэш
		/// </summary>
		/// <param name="element">Добавляемый элемент</param>
		/// <returns>Индекс добавленного элемента</returns>
		public int Add(T element)
		{
			for (int i = 0; i < CacheSize; i++)
				if (element.Equals(Get(i)))
					return i;
			lock (_readWriteLock)
			{
				var index = _storage.Add(element);
				_lifeTimes.Add(DateTime.Now);
				return index;
			}
		}

		/// <summary>
		/// Удаляет просроченные объекты
		/// </summary>
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

		/// <summary>
		/// Получает итератор для использования объекта в foreach
		/// </summary>
		/// <returns></returns>
		public IEnumerator<T> GetEnumerator()
		{
			return new SafeEnumerator<T>(_storage.GetEnumerator(), _readWriteLock);
		}

		/// <summary>
		/// Получает итератор для использования объекта в foreach
		/// </summary>
		/// <returns></returns>
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
			//123123 ну типа исправил
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