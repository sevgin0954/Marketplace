using Marketplace.Domain.Common;
using System;

namespace Marketplace.Domain.Sales.OfferAggregate
{
	public class Offer : AggregateRoot
	{
		public Status Status { get; private set; } = Status.Pending;

		public string SellerId { get; private set; }

		public string BuyerId { get; private set; }

		public string Message { get; private set; }

		public string RejectMessage { get; private set; }

		public void AcceptOffer(string initiatorId)
		{
			this.ValidateInitiator(initiatorId);
			this.ThrowExceptionIfStatusNotPending();

			this.Status = Status.Accepted;
		}

		public void RejectOffer(string initiatorId, string reason)
		{
			this.ValidateInitiator(initiatorId);
			this.ThrowExceptionIfStatusNotPending();

			this.RejectMessage = reason;
			this.Status = Status.Rejected;
		}

		private void ValidateInitiator(string initiatorId)
		{
			if (initiatorId != this.SellerId)
				throw new InvalidOperationException();
		}

		private void ThrowExceptionIfStatusNotPending()
		{
			if (this.Status != Status.Pending)
				throw new InvalidOperationException();
		}
	}
}