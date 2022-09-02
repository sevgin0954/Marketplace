using System;
using System.Collections.Generic;

namespace Marketplace.Domain.Common
{
	public abstract class Id : ValueObject
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

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return this.Value;
		}

		public override string ToString()
		{
			return this.Value;
		}
	}
}
