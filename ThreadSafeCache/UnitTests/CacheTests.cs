using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThreadSafeCache;
using ThreadSafeCache.Exceptions;
using ThreadSafeCache.Storages;

namespace UnitTests
{
	[TestClass]
	public class CacheTests
	{
		[TestMethod]
		public void AddInParallelAndReleaseAll()
		{
			var cache = new Cache<double>(new MemoryListStorage<double>(), TimeSpan.FromSeconds(3));
			var counter1 = 0;
			cache.ObjectDeleted += (sender, args) => counter1++;
			var counter2 = 0;
			cache.ObjectDeleted += (sender, args) => counter2++;
			var random = new Random(DateTime.Now.GetHashCode());
			Parallel.For(0, 1000, i => { cache.Add(random.NextDouble()); });
			Thread.Sleep(3000);
			cache.ReleaseOld();
			Assert.IsTrue(counter1 == 1000);
			Assert.IsTrue(counter2 == 1000);
			Assert.IsTrue(cache.CacheSize == 0);
		}

		[Timeout(5000)]
		[TestMethod]
		public void ChangeAutoReleaseInterval()
		{
			var cache = new Cache<double>(new MemoryListStorage<double>(), TimeSpan.FromSeconds(1), TimeSpan.FromDays(1));
			var random = new Random(DateTime.Now.GetHashCode());
			var counter1 = 0;
			cache.ObjectDeleted += (sender, args) => counter1++;
			Parallel.For(0, 1000, i => { cache.Add(random.NextDouble()); });
			Thread.Sleep(3000);
			Assert.IsTrue(cache.CacheSize == 1000);
			cache.AutoReleaseInterval = TimeSpan.FromSeconds(1);
			while (counter1 != 1000) { }
			Assert.IsTrue(cache.CacheSize == 0);
		}

		[TestMethod]
		public void WasNotDeleted()
		{
			var cache = new Cache<int>(new MemoryListStorage<int>(), TimeSpan.FromSeconds(2));
			cache.Add(5);
			cache.ReleaseOld();
			Assert.IsTrue(cache.CacheSize == 1);
			cache = new Cache<int>(new MemoryListStorage<int>());
			cache.Add(5);
			cache.ReleaseOld();
			Assert.IsTrue(cache.CacheSize == 1);
		}

		[TestMethod]
		public void Gets()
		{
			var cache = new Cache<int>(new MemoryListStorage<int>(), TimeSpan.FromSeconds(2));
			cache.Add(5);
			Assert.IsTrue(cache.Get(0) == 5);
			Assert.IsTrue(cache.GetIfActual(0) == 5);
			Thread.Sleep(3000);
			Assert.IsTrue(cache.Get(0) == 5);
			try
			{
				var value = cache.GetIfActual(0);
			}
			catch (ElementNotActualException)
			{
				return;
			}
			Assert.Inconclusive();
		}

		[TestMethod]
		public void Iterate()
		{
			var cache = new Cache<int>(new MemoryListStorage<int>(), TimeSpan.FromSeconds(2));
			cache.Add(5);
			cache.Add(5);
			foreach (var cached in cache)
			{
				Assert.IsTrue(cached == 5);
			}
		}
	}
}
