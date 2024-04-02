using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.BuyerAggregate.Commands;
using Marketplace.Domain.Sales.OfferAggregate.Commands;
using Marketplace.Domain.Sales.ProductAggregate.Commands;
using Marketplace.Domain.Sales.ProductAggregate.Events;
using Marketplace.Domain.Sales.SellerAggregate.Commands;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using Marketplace.Shared;
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
			IMediator mediator)
			: base(data, mediator)
		{
			this.mediator = mediator;
		}
		// TODO: Create interface for transition
		protected override async Task OnStartSagaAsync()
		{
			var buyerId = this.Data.BuyerId.Value;
			var productId = this.Data.ProductId.Value;
			var sellerId = this.Data.SellerId.Value;
			var initiatorId = this.Data.Id.Value;

			await this.SendCommandOrCompleteAsync(new CheckIsBuyerBannedCommand(sellerId, buyerId));
			await this.SendCommandOrCompleteAsync(new CheckCanBuyProductCommand(productId, initiatorId));
			await this.SendCommandOrCompleteAsync(new StartMakingOfferCommand(buyerId, productId));
		}

		public async Task TransitionAsync(BuyerWasBannedEvent message)
		{
			ArgumentValidator.NotNullValidator(message, nameof(message));

			this.MarkAsComplete();
			await this.DiscardAddingOfferToBuyerAsync();
		}

		public async Task TransitionAsync(BuyerWasNotBannedEvent message)
		{
			ArgumentValidator.NotNullValidator(message, nameof(message));

			this.Data.IsBuyerNotBannedChecked = true;
			await this.TryFinishAddingOfferToBuyerAsync();
		}

		public async Task TransitionAsync(ProductCouldNotBeBoughtEvent message)
		{
			ArgumentValidator.NotNullValidator(message, nameof(message));

			this.MarkAsComplete();
			await this.DiscardAddingOfferToBuyerAsync();
		}

		public async Task TransitionAsync(ProductCouldBeBoughtEvent message)
		{
			ArgumentValidator.NotNullValidator(message, nameof(message));

			this.Data.IsProductEligableForBuyChecked = true;
			await this.TryFinishAddingOfferToBuyerAsync();
		}

		protected override async Task OnCompleteSagaAsync()
		{
			var isEachCheckPassed = this.Data.IsBuyerNotBannedChecked && this.Data.IsProductEligableForBuyChecked;
			if (isEachCheckPassed == false)
				throw new InvalidOperationException("Can't finish saga before all transitions are completed!");

			var makeOfferCommand = new MakeOfferCommand(
				this.Data.BuyerId.Value,
				this.Data.ProductId.Value,
				this.Data.Message,
				this.Data.Quantity,
				this.Data.SellerId.Value
			);

			this.Result = await this.mediator.Send(makeOfferCommand);
		}

		private async Task TryFinishAddingOfferToBuyerAsync()
		{
			var isEachCheckPassed = this.Data.IsBuyerNotBannedChecked && this.Data.IsProductEligableForBuyChecked;
			if (isEachCheckPassed && this.IsCompleted == false)
			{
				var finishMakingOfferCommand =
					new FinishMakingOfferCommand(this.Data.BuyerId.Value, this.Data.ProductId.Value);

				await this.SendCommandOrThrowExceptionAsync(finishMakingOfferCommand);

				await this.CompleteSagaAsync();
			}
		}
		
		private async Task DiscardAddingOfferToBuyerAsync()
		{
			var discardMakingOfferCommand =
				new DiscardMakingOfferCommand(this.Data.BuyerId.Value, this.Data.ProductId.Value);

			await this.SendCommandOrThrowExceptionAsync(discardMakingOfferCommand);
		}
	}
}