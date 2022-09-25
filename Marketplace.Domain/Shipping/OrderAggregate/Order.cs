using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using Marketplace.Domain.SharedKernel;
using System;

namespace Marketplace.Domain.Shipping.OrderAggregate
{
	public class Order : AggregateRoot<Id>
	{
		private string trackingNumber;

		public Order(Id id)
			: base(id)
		{
			this.Status = Status.Processing;
		}

		public Status Status { get; private set; }

		public string TrackingNumber
		{
			get
			{
				return this.trackingNumber;
			}
			set
			{
				if (value.Length > OrderConstants.TrackingNumberMaxLength)
					throw new InvalidOperationException();

				this.trackingNumber = value;
			}
		}

		public Id SellerId { get; private set; }

		public Id BuyerId { get; private set; }

		public CanceledOrderInitiator CanceledOrderInitiator { get; private set; }

		public void RequestCancelOrderByBuyer(Id buyerId)
		{
			if (buyerId != this.BuyerId)
				throw new InvalidOperationException(ErrorConstants.INITIATOR_SHOULD_BE_THE_BUYER);
			if (this.Status != Status.Processing)
				throw new InvalidOperationException("Can't cancel non processing order!");

			this.Status = Status.RequestCanceleByBuyer;
		}

		public void ConfirmCancelOrderRequest(Id initiatorId)
		{
			if (initiatorId != this.SellerId)
				throw new InvalidOperationException(ErrorConstants.INITIATOR_SHOULD_BE_THE_SELLER);
			if (this.Status == Status.RequestCanceleByBuyer)
				throw new InvalidOperationException("Can't confirm non requested cancel!");


			this.Status = Status.Cancelled;
		}

		public void CancelOrderBySeller(Id sellerId)
		{
			if (sellerId != this.BuyerId)
				throw new InvalidOperationException(ErrorConstants.INITIATOR_SHOULD_BE_THE_BUYER);
			if (this.Status == Status.Cancelled)
				throw new InvalidOperationException("Can't cancel an already canceled order!");

			this.Status = Status.Cancelled;
		}

		public void StartShipping(Id initiatorId, string trackingNumber)
		{
			if (initiatorId != this.SellerId)
				throw new InvalidOperationException(ErrorConstants.INITIATOR_SHOULD_BE_THE_SELLER);
			if (this.Status != Status.Processing)
				throw new InvalidOperationException("Can't ship non processing order!");

			this.TrackingNumber = trackingNumber;
			this.Status = Status.Shipped;
		}

		public void ConfirmDelivery(Id initiatorId)
		{
			if (initiatorId != this.BuyerId)
				throw new InvalidOperationException(ErrorConstants.INITIATOR_SHOULD_BE_THE_BUYER);
			if (this.Status != Status.Shipped)
				throw new InvalidOperationException("Can't confirm delivery to a non shipped order!");

			this.Status = Status.Delivered;
		}
	}
}