using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.BuyerAggregate.Commands;
using Marketplace.Domain.Sales.BuyerAggregate.Events;
using Marketplace.Domain.Sales.OfferAggregate.Commands;
using Marketplace.Domain.Sales.ProductAggregate.Commands;
using Marketplace.Domain.Sales.ProductAggregate.Events;
using Marketplace.Domain.Sales.SellerAggregate.Commands;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using MediatR;
using System;
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
			await this.mediator.Send(new StartMakingOfferCommand(this.Data.BuyerId.Value, this.Id.Value));
		}

		public async Task TransitionAsync(BuyerWasBannedEvent message)
		{
			ArgumentValidator.NotNullValidator(message, nameof(message));

			this.IsCompleted = true;
			await this.DiscardAddingOfferToBuyer();
		}

		public async Task TransitionAsync(BuyerWasNotBannedEvent message)
		{
			ArgumentValidator.NotNullValidator(message, nameof(message));

			this.Data.IsBuyerNotBannedChecked = true;
			await this.TryFinishAddingOfferToBuyer();
		}

		public async Task TransitionAsync(ProductCouldNotBeBoughtEvent message)
		{
			ArgumentValidator.NotNullValidator(message, nameof(message));

			this.IsCompleted = true;
			await this.DiscardAddingOfferToBuyer();
		}

		public async Task TransitionAsync(ProductCouldBeBoughtEvent message)
		{
			ArgumentValidator.NotNullValidator(message, nameof(message));

			this.Data.IsProductEligableForBuyChecked = true;
			await this.TryFinishAddingOfferToBuyer();
		}

		public async Task FinishSagaAsync(OfferWasAddedToBuyerEvent message)
		{
			ArgumentValidator.NotNullValidator(message, nameof(message));

			if (this.IsCompleted)
				throw new InvalidOperationException("Can't finish completed saga!");

			var isEachCheckPassed = this.Data.IsBuyerNotBannedChecked && this.Data.IsProductEligableForBuyChecked;
			if (isEachCheckPassed == false)
				throw new InvalidOperationException("Can't finish saga before all transitions are completed!");

			this.IsCompleted = true;

			var makeOfferCommand = new MakeOfferCommand(
				this.Data.BuyerId.Value,
				this.Data.ProductId.Value,
				this.Data.Message,
				this.Data.Quantity,
				this.Data.SellerId.Value
			);

			this.Result = await this.mediator.Send(makeOfferCommand);
		}

		private async Task TryFinishAddingOfferToBuyer()
		{
			var isEachCheckPassed = this.Data.IsBuyerNotBannedChecked && this.Data.IsProductEligableForBuyChecked;
			if (isEachCheckPassed && this.IsCompleted == false)
			{
				var finishMakingOfferCommand = 
					new FinishMakingOfferCommand(this.Data.BuyerId.Value, this.Data.ProductId.Value);

				await this.mediator.Send(finishMakingOfferCommand);
			}
		}

		private async Task DiscardAddingOfferToBuyer()
		{
			var finishMakingOfferCommand = 
				new DiscardMakingOfferCommand(this.Data.BuyerId.Value, this.Data.ProductId.Value);

			await this.mediator.Send(finishMakingOfferCommand);
		}
	}
}