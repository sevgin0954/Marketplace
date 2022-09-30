using Marketplace.Domain.SharedKernel;

namespace Marketplace.Domain.Sales.SellerAggregate
{
	internal record Offer(Id BuyerId, Id ProductId, int Quantity);
}
