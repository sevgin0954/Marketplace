using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.BuyerAggregate.Events;
using Marketplace.Domain.Sales.ProductAggregate.Events;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.BuyerAggregate.Sagas.AddPendingOffer
{
	public class AddPendingOfferSaga : Saga<AddPendingOfferSagaData>, 
		IStartedBy<StartAddingOfferEvent>,
		IHandleEvent<ProductCanBeBoughtEvent>,
		IHandleEvent<BuyerIsNotBannedEvent>
	{
		private readonly IBuyerRepository buyerRepository;

		public AddPendingOfferSaga(IBuyerRepository buyerRepository)
		{
			this.buyerRepository = buyerRepository;
		}

		public string BuyerId { get; private set; }

		public string ProductId { get; private set; }

		public bool ProductCanBeBought => this.Data.ProductCanBeBought;

		public void TransitionFromStart(StartAddingOfferEvent message)
		{
			this.BuyerId = message.BuyerId;
			this.ProductId = message.ProductId;
		}

		public async Task Transition(ProductCanBeBoughtEvent notification)
		{
			this.Data.ProductCanBeBought = true;

			await this.CompleteIfPossible();
		}

		public async Task Transition(BuyerIsNotBannedEvent notification)
		{
			this.Data.BuyerBannedStatusHasBeenChecked = true;

			await this.CompleteIfPossible();
		}

		private async Task CompleteIfPossible()
		{
			if (this.Data.BuyerBannedStatusHasBeenChecked && this.Data.ProductCanBeBought)
			{
				var buyer = await this.buyerRepository.GetByIdAsync(this.BuyerId);
				buyer.AddOffer(this.ProductId);

				this.IsCompleted = true;
			}
		}
	}
}