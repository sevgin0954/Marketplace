using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using Marketplace.Domain.Sales.ProductAggregate.Events;
using Marketplace.Domain.SharedKernel;
using System;

namespace Marketplace.Domain.Sales.ProductAggregate
{
	public class Product : AggregateRoot<Id>
	{
		private decimal price;

		public Product(Id id, decimal price, Id sellerId)
			: base(id)
		{
			this.Price = price;
			this.SellerId = sellerId;
			this.Status = ProductStatus.Unsold;
		}

		public decimal Price
		{
			get { return this.price; }
			private set
			{
				if (value < 0)
					throw new InvalidOperationException(ErrorConstants.NUMBER_CANT_BE_NEGATIVE);

				this.price = value;
			}
		}

		public Id SellerId { get; private set; }

		public ProductStatus Status { get; private set; }

		public void Archive(Id initiatorId)
		{
			if (initiatorId != this.SellerId)
				throw new InvalidOperationException(ErrorConstants.INITIATOR_SHOULD_BE_THE_SELLER);
			if (this.Status == ProductStatus.Archived)
			{
				var exceptionMessage = "Can't archive already archived product!";
				throw new InvalidOperationException(exceptionMessage);
			}

			this.Status = ProductStatus.Archived;
		}

		public void Disarchive()
		{
			if (this.Status != ProductStatus.Archived)
				throw new InvalidOperationException();

			this.Status = ProductStatus.Unsold;
		}

		public void CheckIsEligibleForBuyEventCheck(Id initiatorId)
		{
			if (initiatorId == this.SellerId)
			{
				var exceptionMessage = "Seller cannot buy his own product!";
				this.AddDomainEvent(
					new ProductCouldNotBeBoughtEvent(initiatorId.Value, this.Id.Value, exceptionMessage)
				);
			}
			else if (this.Status != ProductStatus.Unsold)
			{
				var exceptionMessage = "Product is not in sale anymore!";
				this.AddDomainEvent(
					new ProductCouldNotBeBoughtEvent(initiatorId.Value, this.Id.Value, exceptionMessage)
				);
			}
			else
			{
				this.AddDomainEvent(
					new ProductCouldBeBoughtEvent(initiatorId.Value, this.Id.Value));
			}
		}
	}
}