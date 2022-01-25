using Marketplace.Domain.Common;
using System;

namespace Marketplace.Domain.Shipping
{
	public class Order : AggregateRoot
	{
		public Status Status { get; private set; }

		public string TrackingNumber { get; private set; }

		public User Seller { get; private set; }

		public User Buyer { get; private set; }

		public User CanceledBy { get; private set; }

		public void CancelDelivery(User initiator)
		{
			if (initiator != this.Seller || initiator != this.Buyer)
				throw new InvalidOperationException();
			if (this.Status == Status.Cancelled || this.Status == Status.Shipped)
				throw new InvalidOperationException();

			this.Status = Status.Cancelled;
			this.CanceledBy = initiator;
		}

		public void StartDelivery(string trackingNumber)
		{
			if (this.Status != Status.Processing)
				throw new InvalidOperationException();

			this.TrackingNumber = trackingNumber;
			this.Status = Status.Shipped;
		}
	}
}
