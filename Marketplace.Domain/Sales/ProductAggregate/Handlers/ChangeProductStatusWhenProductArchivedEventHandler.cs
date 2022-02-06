using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.SellerAggregate.Events;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.ProductAggregate.Handlers
{
	public class ChangeProductStatusWhenProductArchivedEventHandler : IHandler<ProductArchivedEvent>
	{
		private readonly IRepository<Product> productRepository;

		public ChangeProductStatusWhenProductArchivedEventHandler(
			IRepository<Product> productRepository)
		{
			this.productRepository = productRepository;
		}

		public async Task HandleAsync(ProductArchivedEvent domainEvent)
		{
			var product = await this.productRepository.GetByIdAsync(domainEvent.ProductId);
			product.Status = ProductStatus.Archived;

			await this.productRepository.SaveChangesAsync();
		}
	}
}
