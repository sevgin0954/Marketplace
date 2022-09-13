using System;

namespace Marketplace.Domain.Common.Exceptions
{
	internal class NotFoundException : Exception
	{
		public NotFoundException(string message)
			: base(message) { }
	}
}
