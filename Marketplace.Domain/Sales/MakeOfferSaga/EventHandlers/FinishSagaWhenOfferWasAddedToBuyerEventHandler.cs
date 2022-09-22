using Marketplace.Domain.Sales.BuyerAggregate.Events;
using Marketplace.Domain.Sales.MakeOfferSagaNS;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSaga.EventHandlers
{
	internal class FinishSagaWhenOfferWasAddedToBuyerEventHandler : INotificationHandler<OfferWasAddedToBuyerEvent>
	{
		private readonly IMakeOfferSagaRepository sagaRepository;

		public FinishSagaWhenOfferWasAddedToBuyerEventHandler(
			IMakeOfferSagaRepository sagaRepository)
		{
			this.sagaRepository = sagaRepository;
		}

		public async Task Handle(OfferWasAddedToBuyerEvent notification, CancellationToken cancellationToken)
		{
			var buyerId = new Id(notification.BuyerId);
			var productId = new Id(notification.ProductId);
			var sagaId = new MakeOfferSagaId(buyerId, productId);

			var saga = await this.sagaRepository.GetByIdAsync(sagaId);

			await saga.FinishSagaAsync(notification);

			await this.sagaRepository.SaveChangesAsync();
		}
	}
}
