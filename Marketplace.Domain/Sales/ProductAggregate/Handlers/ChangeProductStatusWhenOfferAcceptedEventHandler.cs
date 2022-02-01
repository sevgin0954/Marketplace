using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.SellerAggregate;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.ProductAggregate.Handlers
{
	public class ChangeProductStatusWhenOfferAcceptedEventHandler : IHandler<OfferAcceptedEvent>
	{
		private readonly IRepository<Product> productRepository;

		public ChangeProductStatusWhenOfferAcceptedEventHandler(
			IRepository<Product> productRepository)
		{
			this.productRepository = productRepository;
		}

		public async Task HandleAsync(OfferAcceptedEvent domainEvent)
		{
			var product = await this.productRepository.GetByIdAsync(domainEvent.ProductId);
			product.Status = ProductStatus.Sold;

			await this.productRepository.SaveChangesAsync();
		}
	}
}
