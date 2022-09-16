using Marketplace.Domain.Sales.ProductAggregate.Events;
using Marketplace.Domain.SharedKernel;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.MakeOfferSagaNS.EventHandlers
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
			var buyerId = new Id(notification.BuyerId);
			var productId = new Id(notification.ProductId);
			var makeOfferSagaId = new MakeOfferSagaId(buyerId, productId);

			var makeOfferSaga = await this.makeOfferSagaRepository.GetByIdAsync(makeOfferSagaId);

			await makeOfferSaga.TransitionAsync(notification);

			await this.makeOfferSagaRepository.SaveChangesAsync();
		}
	}
}
