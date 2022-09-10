using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.ProductAggregate.Events;
using Marketplace.Domain.SharedKernel;
using System;

namespace Marketplace.Domain.Sales.ProductAggregate
{
	public class Product : AggregateRoot<Id>
	{
		const string PRODUCT_NOT_IN_SALE_ANYMORE = "Product is not in sale anymore!";
		const string SELLER_CANT_BUY_HIS_OWN_PRODUCT = "Seller cannot buy his own product";

		public Product(Id id, decimal price, string sellerId)
			: base(id)
		{
			this.Price = price;
			this.SellerId = sellerId;
			this.Status = ProductStatus.Unsold;
		}

		public decimal Price { get; private set; }

		public string SellerId { get; private set; }

		public ProductStatus Status { get; private set; }

		public void Archive()
		{
			if (this.Status == ProductStatus.Archived)
				throw new InvalidOperationException();

			this.Status = ProductStatus.Archived;
		}

		public void Disarchive()
		{
			if (this.Status != ProductStatus.Archived)
				throw new InvalidOperationException();

			this.Status = ProductStatus.Unsold;
		}

		public void CheckIsEligibleForBuyEventCheck(string initiatorId)
		{
			if (initiatorId == this.SellerId)
			{
				this.AddDomainEvent(
					new ProductCouldNotBeBoughtEvent(initiatorId, this.Id.Value, SELLER_CANT_BUY_HIS_OWN_PRODUCT));
			}
			else if (this.Status != ProductStatus.Unsold)
			{
				this.AddDomainEvent(
					new ProductCouldNotBeBoughtEvent(initiatorId, this.Id.Value, PRODUCT_NOT_IN_SALE_ANYMORE));
			}
			else
			{
				this.AddDomainEvent(
					new ProductCouldBeBoughtEvent(initiatorId, this.Id.Value));
			}
		}
	}
}