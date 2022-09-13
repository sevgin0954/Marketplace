using Marketplace.Domain.Sales.ProductAggregate.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSaga.EventHandlers
{
	internal class TransitionProductCouldBeBoughtEventHandler : INotificationHandler<ProductCouldBeBoughtEvent>
	{
		private readonly IMakeOfferSagaRepository makeOfferSagaRepository;

		public TransitionProductCouldBeBoughtEventHandler(IMakeOfferSagaRepository makeOfferSagaRepository)
		{
			this.makeOfferSagaRepository = makeOfferSagaRepository;
		}

		public async Task Handle(ProductCouldBeBoughtEvent notification, CancellationToken cancellationToken)
		{
			var makeOfferSagaId = new MakeOfferSagaId(notification.BuyerId, notification.ProductId);
			var makeOfferSaga = await this.makeOfferSagaRepository.GetByIdAsync(makeOfferSagaId);

			await makeOfferSaga.TransitionAsync(notification);

			await this.makeOfferSagaRepository.SaveChangesAsync();
		}
	}
}
