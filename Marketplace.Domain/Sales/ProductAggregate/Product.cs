using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.ProductAggregate.Events;
using System;

namespace Marketplace.Domain.Sales.ProductAggregate
{
	public class Product : AggregateRoot
	{
		public decimal Price { get; private set; }

		public string SellerId { get; private set; }

		public int Quantity { get; private set; }

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

		public void Buy(int quantity, string buyerId)
		{
			if (this.ProductCanBeBought(quantity))
			{
				this.AddDomainEvent(new UnsuccessfulProductPurchaseEvent(buyerId, this.Id));
				return;
			}

			this.Quantity -= quantity;

			if (this.Quantity == 0)
			{
				this.Status = ProductStatus.Sold;
			}

			this.AddDomainEvent(new SuccessfulProductPurchaseEvent(buyerId, this.Id)); ;
		}

		public void CheckIfProductCanBeBought(int quantity)
		{
			if (this.ProductCanBeBought(quantity))
			{
				this.AddDomainEvent(new ProductCanBeBoughtEvent(this.Id, quantity));
			}
			else
			{
				this.AddDomainEvent(new ProductCannotBeBoughtEvent(this.Id));
			}
		}

		private bool ProductCanBeBought(int quantity)
		{
			if (this.Status != ProductStatus.Unsold)
			{
				return false;
			}

			if (quantity > this.Quantity)
			{
				return false;
			}

			return true;
		}
	}
}