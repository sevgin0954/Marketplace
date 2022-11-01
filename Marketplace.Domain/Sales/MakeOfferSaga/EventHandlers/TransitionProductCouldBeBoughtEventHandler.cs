using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.ProductAggregate.Events;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSagaNS.EventHandlers
{
	internal class TransitionProductCouldBeBoughtEventHandler : INotificationHandler<ProductCouldBeBoughtEvent>
	{
		private readonly ISagaDataRepository<MakeOfferSagaData, MakeOfferSagaId> sagaDataRepository;
		private readonly IMediator mediator;

		public TransitionProductCouldBeBoughtEventHandler(
			ISagaDataRepository<MakeOfferSagaData, MakeOfferSagaId> sagaDataRepository,
			IMediator mediator)
		{
			this.sagaDataRepository = sagaDataRepository;
			this.mediator = mediator;
		}

		public async Task Handle(ProductCouldBeBoughtEvent notification, CancellationToken cancellationToken)
		{
			var buyerId = new Id(notification.BuyerId);
			var productId = new Id(notification.ProductId);
			var makeOfferSagaId = new MakeOfferSagaId(buyerId, productId);

			var sagaData = await this.sagaDataRepository.GetByIdAsync(makeOfferSagaId);

			var saga = new MakeOfferSaga(sagaData, this.mediator);
			await saga.TransitionAsync(notification);

			await this.sagaDataRepository.SaveChangesAsync(cancellationToken);
			// TODO
		}
	}
}
