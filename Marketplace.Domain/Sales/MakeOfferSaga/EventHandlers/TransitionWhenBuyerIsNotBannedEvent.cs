using Marketplace.Domain.Sales.SellerAggregate.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSaga.EventHandlers
{
	internal class TransitionWhenBuyerIsNotBannedEvent : INotificationHandler<BuyerIsNotBannedEvent>
	{
		private readonly IMakeOfferSagaRepository makeOfferSagaRepository;

		public TransitionWhenBuyerIsNotBannedEvent(
			IMakeOfferSagaRepository makeOfferSagaRepository)
		{
			this.makeOfferSagaRepository = makeOfferSagaRepository;
		}

		public async Task Handle(BuyerIsNotBannedEvent notification, CancellationToken cancellationToken)
		{
			var makeOfferSaga = await makeOfferSagaRepository
				.GetAllNotCompletedByIds(notification.BuyerId, notification.SellerId);

			foreach (var currentMakeOfferSaga in makeOfferSaga)
			{
				await currentMakeOfferSaga.TransitionAsync(notification);
				await this.makeOfferSagaRepository.SaveChangesAsync();
			}
		}
	}
}
