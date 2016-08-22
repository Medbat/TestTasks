using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using ThreadSafeCache.Exceptions;
using ThreadSafeCache.Storages;

namespace ThreadSafeCache
{
	public class Test
	{
		public int A { get; set; }
		public StringBuilder B { get; set; }
	}

	internal static class Program
	{
		private static void Main(string[] args)
		{
			var c = new Cache<Test>(new DbStorage<Test>(new DbStorageContext(
				"data source=MSI-FEDOR\\SQLEXPRESS;Initial Catalog=Test;Integrated Security=True;User ID=new_admin;Password=newpass")));
			//var index = c.Add(new Test { A = 25, B = new StringBuilder("lal") });
			var a = c.Get(1);
			Console.WriteLine(a.B.ToString());
			return;
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
