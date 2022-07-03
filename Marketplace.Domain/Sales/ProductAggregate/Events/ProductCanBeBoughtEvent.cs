using MediatR;
using System;

namespace Marketplace.Domain.Sales.ProductAggregate.Events
{
	public record ProductCanBeBoughtEvent : INotification
	{
		public ProductCanBeBoughtEvent(string productId, int quantity)
		{
			this.ProductId = productId;
			this.Quantity = quantity;
		}

		public string ProductId { get; }
		public int Quantity { get; }
	}
}
