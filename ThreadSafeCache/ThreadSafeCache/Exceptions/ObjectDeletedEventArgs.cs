using System;

namespace ThreadSafeCache.Exceptions
{
	public class ObjectDeletedEventArgs : EventArgs
	{
		public int Index;
	}
}