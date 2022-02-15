using AutoMapper;
using Marketplace.Domain.Shipping.OrderAggregate;
using OrderAggregate = Marketplace.Domain.Shipping.OrderAggregate.Order;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure.Shipping.OrderPersistence
{
	public class OrderRepository : IOrderRepository
	{
		private readonly OrderDbContext orderDbContext;
		private readonly IMapper mapper;

		public OrderRepository(
			OrderDbContext orderDbContext,
			IMapper mapper)
		{
			this.orderDbContext = orderDbContext;
			this.mapper = mapper;
		}

		public Task<int> AddAsync(OrderAggregate element)
		{
			throw new System.NotImplementedException();
		}

		public Task<IList<OrderAggregate>> GetAllAsync()
		{
			throw new System.NotImplementedException();
		}

		public async Task<IList<OrderAggregate>> GetByBuyerAsync(string buyerId)
		{
			var orders = await this.orderDbContext.Orders
				.AsQueryable()
				.Where(o => o.BuyerId == buyerId)
				.ToListAsync();

			var ordersAggregates = this.mapper.Map<List<OrderAggregate>>(orders);

			return ordersAggregates;
		}

		public Task<OrderAggregate> GetByIdAsync(string id)
		{
			throw new System.NotImplementedException();
		}

		public Task<int> SaveChangesAsync()
		{
			throw new System.NotImplementedException();
		}
	}
}
