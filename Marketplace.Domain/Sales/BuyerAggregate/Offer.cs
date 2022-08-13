using Marketplace.Domain.Common;
using System;

namespace Marketplace.Domain.Sales.BuyerAggregate
{
	public class Offer : Entity
	{
		public Offer(string productId, string sellerId, string message)
		{
			this.ProductId = productId;
			this.SellerId = sellerId;
			this.Message = message;
		}

		public OfferStatus Status { get; private set; } = OfferStatus.Pending;

		public string ProductId { get; }

		public string SellerId { get; }

		public string Message { get; }

		public string RejectMessage { get; private set; }

		public void AcceptOffer(string initiatorId)
		{
			this.ValidateInitiator(initiatorId);
			this.ThrowExceptionIfStatusNotPending();

			this.Status = OfferStatus.Accepted;
		}

		public void RejectOffer(string initiatorId, string reason)
		{
			this.ValidateInitiator(initiatorId);
			this.ThrowExceptionIfStatusNotPending();

			this.RejectMessage = reason;
			this.Status = OfferStatus.Rejected;
		}

		private void ValidateInitiator(string initiatorId)
		{
			if (initiatorId != this.SellerId)
				throw new InvalidOperationException();
		}

		private void ThrowExceptionIfStatusNotPending()
		{
			if (this.Status != OfferStatus.Pending)
				throw new InvalidOperationException();
		}
	}
}