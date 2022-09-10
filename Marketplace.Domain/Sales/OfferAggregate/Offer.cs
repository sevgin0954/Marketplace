using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using Marketplace.Domain.SharedKernel;
using System;

namespace Marketplace.Domain.Sales.OfferAggregate
{
	public class Offer : AggregateRoot<OfferId>
	{
		const string CANT_DISCARD_NON_PENDING_OFFER = "Can't discard non pending offer!";

		public Offer(OfferId id, Id sellerId, string message)
			: base(id)
		{
			this.SellerId = sellerId;
			this.Message = message;
			this.ProductId = id.ProductId;
			this.BuyerId = id.BuyerId;
		}

		public OfferStatus Status { get; private set; } = OfferStatus.Pending;

		public Id SellerId { get; }

		public string Message { get; }

		public string RejectMessage { get; private set; }

		public Id ProductId { get; }

		public Id BuyerId { get; }

		public void DiscardOffer(Id initiatorId)
		{
			if (initiatorId == null)
				throw new ArgumentNullException(nameof(initiatorId));
			if (initiatorId != this.BuyerId)
				throw new InvalidOperationException(ErrorConstants.BUYER_CANT_BE_THE_INITIATOR);
			if (this.Status != OfferStatus.Pending)
				throw new InvalidOperationException(CANT_DISCARD_NON_PENDING_OFFER);

			this.Status = OfferStatus.Discarded;
		}

		public void AcceptOffer(Id initiatorId)
		{
			this.ValidateInitiatorIsNotTheSeller(initiatorId);
			this.ThrowExceptionIfStatusNotPending();

			this.Status = OfferStatus.Accepted;
		}

		public void RejectOffer(Id initiatorId, string reason)
		{
			this.ValidateInitiatorIsNotTheSeller(initiatorId);
			this.ThrowExceptionIfStatusNotPending();

			this.RejectMessage = reason;
			this.Status = OfferStatus.Rejected;
		}

		private void ValidateInitiatorIsNotTheSeller(Id initiatorId)
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