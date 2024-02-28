using System;

namespace Marketplace.Domain.Common.Exceptions
{
	public class InvalidIdException : Exception
	{
		public InvalidIdException(string objectName)
		{
			this.ParameterName = objectName;
		}

		public string ParameterName { get; }
	}
}
