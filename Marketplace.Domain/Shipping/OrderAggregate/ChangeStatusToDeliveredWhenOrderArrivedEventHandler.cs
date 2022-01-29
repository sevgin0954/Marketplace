﻿using Marketplace.Domain.Common;
using Marketplace.Domain.Shipping.UserAggregate;
using System.Threading.Tasks;

namespace Marketplace.Domain.Shipping.OrderAggregate
{
	class ChangeStatusToDeliveredWhenOrderArrivedEventHandler : IHandler<OrderArrivedEvent>
	{
		private readonly IRepository<Order> orderRepository;

		public ChangeStatusToDeliveredWhenOrderArrivedEventHandler(IRepository<Order> orderRepository)
		{
			this.orderRepository = orderRepository;
		}

		public async Task HandleAsync(OrderArrivedEvent domainEvent)
		{
			var order = await this.orderRepository.GetByIdAsync(domainEvent.OrderId);
			order.ChangeStatusToDelivered(domainEvent.BuyerId);

			await this.orderRepository.SaveChangesAsync();
		}
	}
}
