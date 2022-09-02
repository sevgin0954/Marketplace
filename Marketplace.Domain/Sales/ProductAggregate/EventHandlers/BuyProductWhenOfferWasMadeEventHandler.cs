using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.BuyerAggregate.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.ProductAggregate.EventHandlers
{
	public class BuyProductWhenOfferWasMadeEventHandler : INotificationHandler<OfferWasMadeEvent>
	{
		private readonly IAggregateRepository<Product> productRepository;

		public BuyProductWhenOfferWasMadeEventHandler(IAggregateRepository<Product> productRepository)
		{
			this.productRepository = productRepository;
		}


		public async Task Handle(OfferWasMadeEvent notification, CancellationToken cancellationToken)
		{
			var product = await this.productRepository.GetByIdAsync(notification.ProductId);
			product.Buy(notification.InitiatorId);
		}
	}
}
