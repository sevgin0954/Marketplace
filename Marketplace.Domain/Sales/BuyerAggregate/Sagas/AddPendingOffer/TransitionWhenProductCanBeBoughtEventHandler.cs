using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.ProductAggregate.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.BuyerAggregate.Sagas.AddPendingOffer
{
	public class TransitionWhenProductCanBeBoughtEventHandler : INotificationHandler<ProductCanBeBoughtEvent>
	{
		private readonly ISagaRepository<AddPendingOfferSaga, AddPendingOfferSagaData> sagaRepository;
		private readonly IBuyerRepository buyerRepository;

		public TransitionWhenProductCanBeBoughtEventHandler(
			ISagaRepository<AddPendingOfferSaga, AddPendingOfferSagaData> sagaRepository,
			IBuyerRepository buyerRepository)
		{
			this.sagaRepository = sagaRepository;
			this.buyerRepository = buyerRepository;
		}

		public async Task Handle(ProductCanBeBoughtEvent notification, CancellationToken cancellationToken)
		{
			var buyerIds = this.buyerRepository
				.GetIdsByPendingProductIdAsync(notification.ProductId);

			var sagas = await this.sagaRepository
				.FindAsync(
					s => s.ProductCanBeBought && 
					s.ProductId == notification.ProductId);

			foreach (var saga in sagas)
			{
				saga.Transition(notification);
			}

			await this.sagaRepository.SaveChangesAsync();
		}
	}
}
