using System;

namespace Marketplace.Domain.Common
{
	public record Id : ValueObject
	{
		public Id()
		{
			this.Value = Guid.NewGuid().ToString();
		}

		public Id(string id)
		{
			this.Value = id;
		}

		public string Value { get; }
	}
}
