using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.ProductAggregate.Events;
using System;

namespace Marketplace.Domain.Sales.ProductAggregate
{
	public class Product : AggregateRoot
	{
		public Product(decimal price, string sellerId, int quantity)
		{
			this.Price = price;
			this.SellerId = sellerId;
			this.Quantity = quantity;
			this.Status = ProductStatus.Unsold;
		}

		public decimal Price { get; private set; }

		public string SellerId { get; private set; }

		public ProductStatus Status { get; private set; }

		public int Quantity { get; private set; }

		public void Archive()
		{
			if (this.Status == ProductStatus.Archived)
				throw new InvalidOperationException();

			this.Status = ProductStatus.Archived;
			this.Quantity = 0;
		}

		public void Disarchive(int quantity)
		{
			if (quantity <= 0)
				throw new InvalidOperationException();

			this.Status = ProductStatus.Unsold;
			this.Quantity = quantity;
		}

		public void Buy(int quantity, string initiatorId)
		{
			if (initiatorId == this.SellerId)
				throw new InvalidOperationException();
			if (quantity > this.Quantity)
				throw new InvalidOperationException();

			this.Quantity -= quantity;

			if (this.Quantity == 0)
				this.Status = ProductStatus.SoldOut;

			this.AddDomainEvent(new SuccessfulProductPurchaseEvent(initiatorId, this.Id));
		}

		public void CanProductBeBoughtCheck(int quantity)
		{
			if (this.Status != ProductStatus.Unsold)
			{
				this.AddDomainEvent(new ProductCanBeBoughtEvent(this.Id, quantity));
			}
			else
			{
				this.AddDomainEvent(new ProductCannotBeBoughtEvent(this.Id));
			}
		}
	}
}