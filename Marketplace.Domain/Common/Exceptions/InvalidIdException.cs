using System;

namespace Marketplace.Domain.Common.Exceptions
{
	public class InvalidIdException : Exception
	{
		public InvalidIdException(string objectName)
			: base(nameof(InvalidIdException) + " " + objectName) { }
	}
}
