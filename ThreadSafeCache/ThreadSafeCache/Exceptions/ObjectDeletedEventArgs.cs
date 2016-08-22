using System;

namespace ThreadSafeCache.Exceptions
{
	/// <summary>
	/// Класс для передачи данных об удаленном элементе кэша подписчикам
	/// </summary>
	public class ObjectDeletedEventArgs : EventArgs
	{
		public int Index;
	}
}