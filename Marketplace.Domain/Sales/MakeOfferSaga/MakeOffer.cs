using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.ProductAggregate.Events;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using MediatR;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSaga
{
	public class MakeOffer : Saga<MakeOfferSagaData>
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

		public void Transition(BuyerIsBannedEvent message)
		{
			this.isCompleted = true;
		}

		public async Task TransitionAsync(BuyerIsNotBannedEvent message)
		{
			this.Data.IsBuyerBanChecked = true;
			await this .TryCompleteSagaAsync();
		}

		public void Transition(ProductCouldNotBeBoughtEvent message)
		{
			this.isCompleted = true;
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
				this.isCompleted = true;

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