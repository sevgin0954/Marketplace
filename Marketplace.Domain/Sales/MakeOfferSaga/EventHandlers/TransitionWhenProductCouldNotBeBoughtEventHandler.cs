using Marketplace.Domain.Sales.ProductAggregate.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSaga.EventHandlers
{
	public class TransitionWhenProductCouldNotBeBoughtEventHandler : INotificationHandler<ProductCouldNotBeBoughtEvent>
	{
		private readonly IMakeOfferSagaRepository makeOfferSagaRepository;

		public TransitionWhenProductCouldNotBeBoughtEventHandler(IMakeOfferSagaRepository makeOfferSagaRepository)
		{
			this.makeOfferSagaRepository = makeOfferSagaRepository;
		}

		public async Task Handle(ProductCouldNotBeBoughtEvent notification, CancellationToken cancellationToken)
		{
			var makeOfferSagaId = new MakeOfferSagaId(notification.BuyerId, notification.ProductId);
			var makeOfferSaga = await this.makeOfferSagaRepository.GetByIdAsync(makeOfferSagaId.Value);

			makeOfferSaga.Transition(notification);

			await this.makeOfferSagaRepository.SaveChangesAsync();
		}
	}
}
