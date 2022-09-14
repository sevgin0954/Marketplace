using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.ProductAggregate.Commands;
using Marketplace.Domain.Sales.ProductAggregate.Events;
using Marketplace.Domain.Sales.SellerAggregate.Commands;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using MediatR;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSaga
{
	internal class MakeOffer : Saga<MakeOfferSagaData>
	{
		private readonly Mediator mediator;

		public MakeOffer(
			MakeOfferSagaData data,
			MakeOfferSagaId id,
			Mediator mediator)
			: base(id, data)
		{
			this.mediator = mediator;
		}

		public async Task StartSagaAsync()
		{
			await this.mediator.Send(new CheckIsBuyerBannedCommand(this.Data.SellerId, this.Data.BuyerId));
			await this.mediator.Send(new CheckCanBuyProductCommand(this.Data.ProductId, this.Id.Value));
		}

		public void Transition(BuyerWasBannedEvent message)
		{
			this.IsCompleted = true;
		}

		public async Task TransitionAsync(BuyerWasNotBannedEvent message)
		{
			this.Data.IsBuyerBanChecked = true;
			await this .TryCompleteSagaAsync();
		}

		public void Transition(ProductCouldNotBeBoughtEvent message)
		{
			this.IsCompleted = true;
		}

		public async Task TransitionAsync(ProductCouldBeBoughtEvent message)
		{
			this.Data.IsBuyerBanChecked = true;
			await this.TryCompleteSagaAsync();
		}

		private async Task TryCompleteSagaAsync()
		{
			if (this.Data.IsBuyerBanChecked && this.Data.IsProductEligableForBuyChecked)
			{
				this.IsCompleted = true;

				var makeOfferCommand = new OfferAggregate.Commands.MakeOfferCommand(
					this.Data.BuyerId,
					this.Data.ProductId,
					this.Data.Message,
					this.Data.Quantity,
					this.Data.SellerId
				);

				this.Result = await this.mediator.Send(makeOfferCommand);
			}
		}
	}
}