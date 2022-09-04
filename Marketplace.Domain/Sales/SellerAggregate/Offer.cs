using Marketplace.Domain.Common;

namespace Marketplace.Domain.Sales.SellerAggregate
{
	internal record Offer(string BuyerId, string ProductId, int Quantity) : ValueObject;
}
