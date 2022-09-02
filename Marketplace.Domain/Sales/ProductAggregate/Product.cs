using Marketplace.Domain.Common;
using System;

namespace Marketplace.Domain.Sales.ProductAggregate
{
	public class Product : AggregateRoot
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
				this.AddDomainEvent(new );
			}
				// result = Result.Fail(SELLER_CANT_BUY_HIS_OWN_PRODUCT);
			else if (this.Status != ProductStatus.Unsold)
				// result = Result.Fail(PRODUCT_NOT_IN_SALE_ANYMORE);
		}
	}
}