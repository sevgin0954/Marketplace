using BrowsingProduct = Marketplace.Domain.Browsing.ProductAggregate;
using SalesContextProduct = Marketplace.Domain.Sales.ProductAggregate;
using System.Threading.Tasks;
using System.Threading;
using Marketplace.Domain.SharedKernel;
using Marketplace.Domain.Browsing.ProductAggregate;
using Marketplace.Domain.Common.Exceptions;

namespace Marketplace.Domain.Common.Services
{
	public class ProductService : IProductService
	{
		private readonly IRepository<Product, Id> browsingProductRepository;
		private readonly IRepository<SalesContextProduct.Product, Id> salesProductRepository;

		public ProductService(
			IRepository<BrowsingProduct.Product, Id> browsingProductRepository,
			IRepository<SalesContextProduct.Product, Id> salesProductRepository)
		{
			this.browsingProductRepository = browsingProductRepository;
			this.salesProductRepository = salesProductRepository;
		}

		public async Task<bool> CreateAsync(
			BrowsingProduct.Product browsingProduct,
			SalesContextProduct.Product salesProduct, 
			CancellationToken cancellationToken)
		{
			var isSuccessfullyCreated = true;
			
			this.salesProductRepository.Add(salesProduct);
			var isSalesProductAddedSuccessfully = await this.salesProductRepository.SaveChangesAsync(cancellationToken);
			if (isSalesProductAddedSuccessfully == false)
			{
				isSuccessfullyCreated = false;
				return isSuccessfullyCreated;
			}

			this.browsingProductRepository.Add(browsingProduct);
			var isBrowsingProductAddedSuccessfully = await this.browsingProductRepository.SaveChangesAsync(cancellationToken);
			if (isBrowsingProductAddedSuccessfully == false)
			{
				this.salesProductRepository.Remove(salesProduct);
				var isSalesProductRemovedSuccessfully = await this.salesProductRepository.SaveChangesAsync(cancellationToken);
				if (isSalesProductRemovedSuccessfully == false)
					throw new NotPersistentException(nameof(salesProduct));

				isSuccessfullyCreated = false;
			}

			return isSuccessfullyCreated;
		}
	}
}
