using Marketplace.Domain.Common;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.ProductAggregate.EventHandlers
{
	public class BuyProductWhenPendingOfferExistsEventHandler : IHandler<PendingOfferExistsEvent>
	{
		private readonly IRepository<Product> productRepository;

		public BuyProductWhenPendingOfferExistsEventHandler(IRepository<Product> productRepository)
		{
			this.productRepository = productRepository;
		}

		public async Task HandleAsync(PendingOfferExistsEvent domainEvent)
		{
			var product = await this.productRepository.GetByIdAsync(domainEvent.ProductId);
			product.Buy(domainEvent.Quantity, domainEvent.BuyerId);

			await this.productRepository.SaveChangesAsync();
		}
	}
}