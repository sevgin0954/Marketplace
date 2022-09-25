namespace Marketplace.Domain.Shipping.OrderAggregate
{
	public enum Status
	{
		Delivered,
		Shipped,
		Processing,
		Cancelled,
		RequestCanceleByBuyer
	}
}
