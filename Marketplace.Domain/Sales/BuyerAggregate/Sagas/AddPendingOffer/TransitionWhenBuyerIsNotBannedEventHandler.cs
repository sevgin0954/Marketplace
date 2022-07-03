using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.BuyerAggregate.Sagas.AddPendingOffer
{
	public class TransitionWhenBuyerIsNotBannedEventHandler : INotificationHandler<BuyerIsNotBannedEvent>
	{
		private readonly ISagaRepository<AddPendingOfferSaga, AddPendingOfferSagaData> sagaRepository;

		public TransitionWhenBuyerIsNotBannedEventHandler(
			ISagaRepository<AddPendingOfferSaga, AddPendingOfferSagaData> sagaRepository)
		{
			this.sagaRepository = sagaRepository;
		}

		public async Task Handle(BuyerIsNotBannedEvent notification, CancellationToken cancellationToken)
		{
			var sagas = await this.sagaRepository.FindAsync(s => s.ProductCanBeBought == false);
			foreach(var saga in sagas)
			{
				await saga.Transition(notification);
			}

			await this.sagaRepository.SaveChangesAsync();
		}
	}
}
