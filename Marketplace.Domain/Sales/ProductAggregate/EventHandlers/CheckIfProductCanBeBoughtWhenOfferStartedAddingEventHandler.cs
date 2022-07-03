using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.BuyerAggregate.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.ProductAggregate.EventHandlers
{
	public class CheckIfProductCanBeBoughtWhenOfferStartedAddingEventHandler : INotificationHandler<PendingOffersCountIncreasedByOne>
	{
		private readonly IRepository<Product> productRepository;

		public CheckIfProductCanBeBoughtWhenOfferStartedAddingEventHandler(IRepository<Product> productRepository)
		{
			this.productRepository = productRepository;
		}

		public async Task Handle(PendingOffersCountIncreasedByOne notification, CancellationToken cancellationToken)
		{
			var product = await this.productRepository.GetByIdAsync(notification.ProductId);

			product.CheckIfProductCanBeBought(notification.Quantity);
		}
	}
}
