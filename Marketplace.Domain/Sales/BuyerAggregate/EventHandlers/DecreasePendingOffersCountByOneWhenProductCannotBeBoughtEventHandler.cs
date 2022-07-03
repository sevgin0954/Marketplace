using Marketplace.Domain.Sales.ProductAggregate.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.BuyerAggregate.EventHandlers
{
	public class DecreasePendingOffersCountByOneWhenProductCannotBeBoughtEventHandler : INotificationHandler<ProductCannotBeBoughtEvent>
	{
		private readonly IBuyerRepository buyerRepository;

		public DecreasePendingOffersCountByOneWhenProductCannotBeBoughtEventHandler(IBuyerRepository buyerRepository)
		{
			this.buyerRepository = buyerRepository;
		}

		public async Task Handle(ProductCannotBeBoughtEvent notification, CancellationToken cancellationToken)
		{
			//var product = await this.buyerRepository.GetByPendingProductIdAsync(notification.ProductId);

			//product.DecreasePendingOffersCountByOne();

			//await this.buyerRepository.SaveChangesAsync();
		}
	}
}
