using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.ProductAggregate.Commands;
using Marketplace.Domain.Sales.ProductAggregate.Events;
using Marketplace.Domain.Sales.SellerAggregate.Commands;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using MediatR;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSagaNS
{
	internal class MakeOfferSaga : Saga<MakeOfferSagaData>
	{
		private readonly IMediator mediator;

		public MakeOfferSaga(
			MakeOfferSagaData data,
			MakeOfferSagaId id,
			IMediator mediator)
			: base(id, data)
		{
			this.mediator = mediator;
		}

		public override async Task OnStartSagaAsync()
		{
			await this.mediator.Send(new CheckIsBuyerBannedCommand(this.Data.SellerId.Value, this.Data.BuyerId.Value));
			await this.mediator.Send(new CheckCanBuyProductCommand(this.Data.ProductId.Value, this.Id.Value));
		}

		public void Transition(BuyerWasBannedEvent message)
		{
			this.IsCompleted = true;
		}

		public async Task TransitionAsync(BuyerWasNotBannedEvent message)
		{
			this.Data.IsBuyerNotBannedChecked = true;
			await this.TryCompleteSagaAsync();
		}

		public void Transition(ProductCouldNotBeBoughtEvent message)
		{
			this.IsCompleted = true;
		}

		public async Task TransitionAsync(ProductCouldBeBoughtEvent message)
		{
			this.Data.IsProductEligableForBuyChecked = true;
			await this.TryCompleteSagaAsync();
		}

		private async Task TryCompleteSagaAsync()
		{
			if (this.Data.IsBuyerNotBannedChecked && this.Data.IsProductEligableForBuyChecked)
			{
				this.IsCompleted = true;

				var makeOfferCommand = new OfferAggregate.Commands.MakeOfferCommand(
					this.Data.BuyerId.Value,
					this.Data.ProductId.Value,
					this.Data.Message,
					this.Data.Quantity,
					this.Data.SellerId.Value
				);

				this.Result = await this.mediator.Send(makeOfferCommand);
			}
		}
	}
}