using System;

namespace Marketplace.Domain.Common.Exceptions
{
	internal class NotFoundException : Exception
	{
		public NotFoundException(string objectName)
		{
			this.ParameterName = objectName;
		}

		public string ParameterName { get; }
	}
}
