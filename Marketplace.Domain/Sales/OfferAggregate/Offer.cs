using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using Marketplace.Domain.SharedKernel;
using System;

namespace Marketplace.Domain.Sales.OfferAggregate
{
	public class Offer : AggregateRoot<OfferId>
	{
		const string CANT_DISCARD_NON_PENDING_OFFER = "Can't discard non pending offer!";
		const string CANT_ACCEPT_NON_PENDING_OFFER = "Can't accept non pending offer!";
		const string CANT_REJECT_NON_PENDING_OFFER = "Can't reject non pending offer!";

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
			this.ThrowExceptionIfStatusNotPending(CANT_DISCARD_NON_PENDING_OFFER);

			this.Status = OfferStatus.Discarded;
		}

		public void AcceptOffer(Id initiatorId)
		{
			if (initiatorId == null)
				throw new ArgumentNullException(nameof(initiatorId));

			this.ValidateIfInitiatorIsTheSeller(initiatorId);
			this.ThrowExceptionIfStatusNotPending(CANT_ACCEPT_NON_PENDING_OFFER);

			this.Status = OfferStatus.Accepted;
		}

		public void RejectOffer(Id initiatorId, string reason)
		{
			this.ValidateIfInitiatorIsTheSeller(initiatorId);
			this.ThrowExceptionIfStatusNotPending(CANT_REJECT_NON_PENDING_OFFER);

			this.RejectMessage = reason;
			this.Status = OfferStatus.Rejected;
		}

		private void ValidateIfInitiatorIsTheSeller(Id initiatorId)
		{
			if (initiatorId != this.SellerId)
				throw new InvalidOperationException(ErrorConstants.INITIATOR_SHOULD_BE_THE_SELLER);
		}

		private void ThrowExceptionIfStatusNotPending(string message)
		{
			if (this.Status != OfferStatus.Pending)
				throw new InvalidOperationException(message);
		}
	}
}