using System.ComponentModel.DataAnnotations;

namespace ThreadSafeCache.Storages
{
	public class DbStorageEntity
	{
		[Key]
		public int ID { get; set; }

		public string Value { get; set; }
	}
}