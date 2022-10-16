using Marketplace.Domain.Common;
using Marketplace.Domain.Common.Constants;
using Marketplace.Domain.SharedKernel;
using System;

namespace Marketplace.Domain.Sales.OfferAggregate
{
	public class Offer : AggregateRoot<OfferId>
	{
		private string message;
		private string rejectMessage;

		public Offer(OfferId id, Id sellerId, string message)
			: base(id)
		{
			this.SellerId = sellerId;
			this.Message = message;
			this.ProductId = id.ProductId;
			this.BuyerId = id.BuyerId;
			this.Status = OfferStatus.Pending;
		}

		public OfferStatus Status { get; private set; }

		public Id SellerId { get; }

		public string Message
		{
			get { return this.message; }
			init
			{
				ArgumentValidator.NotNullValidator(value, nameof(RejectMessage));
				ArgumentValidator.MaxLength(value, OfferConstants.MESSAGE_MAX_LENGTH, nameof(this.Message));

				this.message = value;
			}
		}

		public string RejectMessage
		{
			get { return this.rejectMessage; }
			set
			{
				ArgumentValidator.NotNullValidator(value, nameof(RejectMessage));
				ArgumentValidator.MaxLength(value, OfferConstants.MESSAGE_MAX_LENGTH, nameof(this.Message));

				this.rejectMessage = value;
			}
		}

		public Id ProductId { get; }

		public Id BuyerId { get; }

		public void DiscardOffer(Id initiatorId)
		{
			ArgumentValidator.NotNullValidator(initiatorId, nameof(initiatorId));
			if (initiatorId != this.BuyerId)
				throw new InvalidOperationException(ErrorConstants.INITIATOR_SHOULD_BE_THE_BUYER);
			this.ThrowExceptionIfStatusNotPending(OfferConstants.CANT_DISCARD_NON_PENDING_OFFER);

			this.Status = OfferStatus.Discarded;
		}

		public void AcceptOffer(Id initiatorId)
		{
			ArgumentValidator.NotNullValidator(initiatorId, nameof(initiatorId));
			this.ThrowExceptionIfInitiatorIsNotTheSeller(initiatorId);
			this.ThrowExceptionIfStatusNotPending(OfferConstants.CANT_ACCEPT_NON_PENDING_OFFER);

			this.Status = OfferStatus.Accepted;
		}

		public void RejectOffer(Id initiatorId, string reason)
		{
			ArgumentValidator.NotNullValidator(initiatorId, nameof(initiatorId));
			ArgumentValidator.NotNullOrEmpty(reason, nameof(reason));

			this.ThrowExceptionIfInitiatorIsNotTheSeller(initiatorId);
			this.ThrowExceptionIfStatusNotPending(OfferConstants.CANT_REJECT_NON_PENDING_OFFER);

			this.RejectMessage = reason;
			this.Status = OfferStatus.Rejected;
		}

		private void ThrowExceptionIfInitiatorIsNotTheSeller(Id initiatorId)
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