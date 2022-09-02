using Marketplace.Domain.Common;
using System;

namespace Marketplace.Domain.Sales.OfferAggregate
{
	public class Offer : AggregateRoot
	{
		public Offer(OfferId id, string productId, string sellerId, string message, int quantity)
			: base(id)
		{
			this.ProductId = productId;
			this.SellerId = sellerId;
			this.Message = message;
			this.Quantity = quantity;
		}

		public OfferStatus Status { get; private set; } = OfferStatus.Pending;

		public string ProductId { get; }

		public string SellerId { get; }

		public string Message { get; }

		public string RejectMessage { get; private set; }

		public int Quantity { get; private set; }

		public string BuyerId { get; }

		public void DiscardOffer(string initiatorId)
		{
			if (initiatorId != this.BuyerId)
				throw new InvalidOperationException();

			this.Status = OfferStatus.Discarded;
		}

		public void AcceptOffer(string initiatorId)
		{
			this.ValidateInitiatorIsNotTheSeller(initiatorId);
			this.ThrowExceptionIfStatusNotPending();

			this.Status = OfferStatus.Accepted;
		}

		public void RejectOffer(string initiatorId, string reason)
		{
			this.ValidateInitiatorIsNotTheSeller(initiatorId);
			this.ThrowExceptionIfStatusNotPending();

			this.RejectMessage = reason;
			this.Status = OfferStatus.Rejected;
		}

		private void ValidateInitiatorIsNotTheSeller(string initiatorId)
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