using Marketplace.Domain.Common;
using System;

namespace Marketplace.Domain.Shipping.OrderAggregate
{
	public class Order : AggregateRoot
	{
		private string trackingNumber;

		public Order()
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

		public string SellerId { get; private set; }

		public string BuyerId { get; private set; }

		public string CanceledById { get; private set; }

		public void CancelDelivery(string initiatorId)
		{
			if (initiatorId != this.SellerId || initiatorId != this.BuyerId)
				throw new InvalidOperationException();
			if (this.Status == Status.Cancelled || this.Status == Status.Shipped)
				throw new InvalidOperationException();

			this.Status = Status.Cancelled;
			this.CanceledById = initiatorId;
		}

		public void StartShipping(string initiatorId, string trackingNumber)
		{
			if (initiatorId != this.SellerId)
				throw new InvalidOperationException();
			if (this.Status != Status.Processing)
				throw new InvalidOperationException();

			this.TrackingNumber = trackingNumber;
			this.Status = Status.Shipped;
		}

		public void ChangeStatusToDelivered(string initiatorId)
		{
			if (initiatorId != this.BuyerId)
				throw new InvalidOperationException();
			if (this.Status != Status.Shipped)
				throw new InvalidOperationException();

			this.Status = Status.Delivered;
		}
	}
}