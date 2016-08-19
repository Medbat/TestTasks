using System;
using System.Runtime.Serialization;

namespace ThreadSafeCache.Exceptions
{
	public class ElementNotActualException : Exception
	{
		public ElementNotActualException() { }

		public ElementNotActualException(string message) : base(message) { }

		public ElementNotActualException(string message, Exception inner) : base(message, inner) { }

		protected ElementNotActualException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}