using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSagaNS.EventHandlers
{
    internal class TransitionWhenBuyerWasNotBannedEvent : INotificationHandler<BuyerWasNotBannedEvent>
	{
		private readonly IRepository<MakeOfferSagaData, MakeOfferSagaId> sagaDataRepository;
		private readonly IMediator mediator;

		public TransitionWhenBuyerWasNotBannedEvent(
			IRepository<MakeOfferSagaData, MakeOfferSagaId> sagaDataRepository,
			IMediator mediator)
		{
			this.sagaDataRepository = sagaDataRepository;
			this.mediator = mediator;
		}

		public async Task Handle(BuyerWasNotBannedEvent notification, CancellationToken cancellationToken)
		{
			var sagaDatas = await this.sagaDataRepository
				.FindAsync(sd => sd.SellerId.Value == notification.SellerId && sd.BuyerId.Value == notification.BuyerId);

			foreach (var currentSagaData in sagaDatas)
			{
				var currentSaga = new MakeOfferSaga(currentSagaData, this.mediator);
				await currentSaga.TransitionAsync(notification);
				await this.sagaDataRepository.SaveChangesAsync(cancellationToken);
				// TODO
			}
		}
	}
}
