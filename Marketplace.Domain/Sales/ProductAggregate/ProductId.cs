using System;
using Tactical.DDD;

namespace Marketplace.Domain.Sales.ProductAggregate
{
	public record ProductId : EntityId
	{
		public ProductId(string id)
		{
			this.Id = id;
		}

		public string Id { get; }
	}
}
