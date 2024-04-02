using BrowsingProduct = Marketplace.Domain.Browsing.ProductAggregate;
using SalesContextProduct = Marketplace.Domain.Sales.ProductAggregate;
using System.Threading.Tasks;
using System.Threading;

namespace Marketplace.Domain.Common.Services
{
	public interface IProductService
	{
		Task<bool> CreateAsync(
			BrowsingProduct.Product browsingProduct,
			SalesContextProduct.Product salesProduct,
			CancellationToken cancellationToken);
	}
}
