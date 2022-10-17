using System;

namespace Marketplace.Domain.SharedKernel
{
	public record Id
	{
		// TODO: Remove, because when inheriting the derived class can forget to call the correct constructor.
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
