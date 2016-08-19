using System;
using System.Threading;
using ThreadSafeCache.Exceptions;
using ThreadSafeCache.Storages;

namespace ThreadSafeCache
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			var cache = new Cache<int>(new MemoryListStorage<int>(), TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
			cache.ObjectDeleted += OnObjectDeleted;
			cache.Add(1);
			Thread.Sleep(1000);
			cache.Add(1212);
			Console.ReadKey();
		}

		private static void OnObjectDeleted(object sender, ObjectDeletedEventArgs args)
		{
			Console.WriteLine(args.Index);
		}
	}
}
