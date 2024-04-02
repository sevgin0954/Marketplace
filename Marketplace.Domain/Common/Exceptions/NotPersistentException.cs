using System;

namespace Marketplace.Domain.Common.Exceptions
{
	public class NotPersistentException : Exception
	{
		public NotPersistentException(params string[] objectNames)
			: base(String.Join(", ", objectNames)) { }
	}
}
