using System;

namespace Marketplace.Domain.Common.Exceptions
{
	public class NotPersistentException : Exception
	{
		public NotPersistentException(string objectName)
		{
			this.ObjectName = objectName;
		}

		public string ObjectName { get; }
	}
}
