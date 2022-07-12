using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.ProductAggregate.Events;
using System;

namespace Marketplace.Domain.Sales.ProductAggregate
{
	public class Product : AggregateRoot
	{
		public decimal Price { get; private set; }

		public string SellerId { get; private set; }

		public ProductStatus Status { get; private set; }

		public void Edit(Product editedProduct)
		{
			if (editedProduct.SellerId != this.SellerId)
				throw new InvalidOperationException();

			this.Price = editedProduct.Price;
			this.Quantity = editedProduct.Quantity;
			this.Status = editedProduct.Status;
		}

		public void Archive()
		{
			if (this.Status != ProductStatus.Unsold)
				throw new InvalidOperationException();

			this.Status = ProductStatus.Archived;
		}

		public void RaiseEventProductCanBeBought(int quantity)
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