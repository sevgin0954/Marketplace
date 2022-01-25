using Marketplace.Domain.Common;
using Marketplace.Domain.Sales.Events;
using System.Threading.Tasks;

namespace Marketplace.Domain.Sales.ProductAggregate
{
	public class ChangeProductStatusWhenPublishedForSaleEventHandler : IHandler<ProbuctPublishedForSaleEvent>
	{
		private readonly IRepository<Product> productRepository;

		public ChangeProductStatusWhenPublishedForSaleEventHandler(IRepository<Product> productRepository)
		{
			this.productRepository = productRepository;
		}

		public async Task HandleAsync(ProbuctPublishedForSaleEvent domainEvent)
		{
			var product = await this.productRepository.GetByIdAsync(domainEvent.ProductId);
			product.Status = ProductStatus.Unsold;


			await this.productRepository.SaveChangesAsync();
		}
	}
}
