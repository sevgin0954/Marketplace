using System;

namespace Marketplace.Domain.SharedKernel
{
	public record Id
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
