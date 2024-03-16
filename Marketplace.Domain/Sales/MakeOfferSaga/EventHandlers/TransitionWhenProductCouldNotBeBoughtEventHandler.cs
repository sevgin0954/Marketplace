using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.ProductAggregate.Events;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSagaNS.EventHandlers
{
	internal class TransitionWhenProductCouldNotBeBoughtEventHandler : INotificationHandler<ProductCouldNotBeBoughtEvent>
	{
		private readonly IRepository<MakeOfferSagaData, MakeOfferSagaId> sagaDataRepository;
		private readonly IMediator mediator;

		public TransitionWhenProductCouldNotBeBoughtEventHandler(
			IRepository<MakeOfferSagaData, MakeOfferSagaId> sagaDataRepository,
			IMediator mediator)
		{
			this.sagaDataRepository = sagaDataRepository;
			this.mediator = mediator;
		}

		public async Task Handle(ProductCouldNotBeBoughtEvent notification, CancellationToken cancellationToken)
		{
			var buyerId = new Id(notification.BuyerId);
			var productId = new Id(notification.ProductId);
			var sagaId = new MakeOfferSagaId(buyerId, productId);

			var sagaData = await this.sagaDataRepository.GetByIdAsync(sagaId);

			var saga = new MakeOfferSaga(sagaData, this.mediator);
			await saga.TransitionAsync(notification);

			await this.sagaDataRepository.SaveChangesAsync(cancellationToken);
			// TODO
		}
	}
}
