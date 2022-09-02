using Marketplace.Domain.Sales.SellerAggregate.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSaga.EventHandlers
{
	public class TransitionWhenBuyerIsBannedEventHandler : INotificationHandler<BuyerIsBannedEvent>
	{
		private readonly IMakeOfferSagaRepository makeOfferSagaRepository;

		public TransitionWhenBuyerIsBannedEventHandler(
			IMakeOfferSagaRepository makeOfferSagaRepository)
		{
			this.makeOfferSagaRepository = makeOfferSagaRepository;
		}

		public async Task Handle(BuyerIsBannedEvent notification, CancellationToken cancellationToken)
		{
			var makeOfferSaga = await makeOfferSagaRepository
				.GetAllNotCompletedByIds(notification.BuyerId, notification.SellerId);
			
			foreach (var currentMakeOfferSaga in makeOfferSaga)
			{
				currentMakeOfferSaga.Transition(notification);
				await this.makeOfferSagaRepository.SaveChangesAsync();
			}
		}
	}
}
