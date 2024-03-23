using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using Marketplace.Domain.Sales.ProductAggregate.Events;
using Marketplace.Domain.SharedKernel;
using Marketplace.Shared;
using System;

namespace Marketplace.Domain.Sales.ProductAggregate
{
	public class Product : AggregateRoot
	{
		private Price price = null!;

		public Product(Id id, Price price, Id sellerId)
			: base(id)
		{
			ArgumentValidator.NotNullValidator(sellerId, nameof(sellerId));

			this.Price = price;
			this.SellerId = sellerId;
			this.Status = ProductStatus.Unsold;
		}

		public Id SellerId { get; }

		public Price Price
		{
			get { return this.price; }
			private set
			{
				ArgumentValidator.NotNullValidator(value, nameof(price));
				ArgumentValidator.NotNegative(value.Value, nameof(price));

				this.price = value;
			}
		}

		public ProductStatus Status { get; private set; }

		public void Archive(Id initiatorId)
		{
			ArgumentValidator.NotNullValidator(initiatorId, nameof(initiatorId));
			this.ThrowExceptionIfInitiatorNotValid(initiatorId);

			if (this.Status == ProductStatus.Archived)
			{
				var exceptionMessage = "Can't archive already archived product!";
				throw new InvalidOperationException(exceptionMessage);
			}

			this.Status = ProductStatus.Archived;
		}

		public void Disarchive(Id initiatorId)
		{
			ArgumentValidator.NotNullValidator(initiatorId, nameof(initiatorId));
			this.ThrowExceptionIfInitiatorNotValid(initiatorId);

			if (this.Status != ProductStatus.Archived)
				throw new InvalidOperationException();

			this.Status = ProductStatus.Unsold;
		}

		public void ChangePrice(Id initiatorId, Price price)
		{
			if (initiatorId != this.SellerId)
				throw new InvalidOperationException(ErrorConstants.INITIATOR_SHOULD_BE_THE_SELLER);

			this.price = price;
		}

		public void CheckIsEligibleForBuyEventCheck(Id initiatorId)
		{
			ArgumentValidator.NotNullValidator(initiatorId, nameof(initiatorId));
			this.ThrowExceptionIfInitiatorNotValid(initiatorId);

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

		private void ThrowExceptionIfInitiatorNotValid(Id initiatorId)
		{
			if (initiatorId != this.SellerId)
				throw new InvalidOperationException(ErrorConstants.INITIATOR_SHOULD_BE_THE_SELLER);
		}
	}
}