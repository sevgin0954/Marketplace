using MediatR;
using System;

namespace Marketplace.Domain.Sales.ProductAggregate.Events
{
	public record ProductCannotBeBoughtEvent : INotification
	{
		public ProductCannotBeBoughtEvent(string productId)
		{
			this.ProductId = productId;
		}

		public string ProductId { get; }
	}
}
