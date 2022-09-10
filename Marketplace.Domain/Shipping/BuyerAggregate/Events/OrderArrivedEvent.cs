using MediatR;

namespace Marketplace.Domain.Shipping.BuyerAggregate.Events
{
	public class OrderArrivedEvent : INotification
	{
		public OrderArrivedEvent(string orderId, string buyerId)
		{
			this.OrderId = orderId;
			this.BuyerId = buyerId;
		}

		public string OrderId { get; }

		public string BuyerId { get; }
	}
}
