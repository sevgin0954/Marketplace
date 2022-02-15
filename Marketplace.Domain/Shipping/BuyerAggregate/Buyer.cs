using Marketplace.Domain.Common;
using Marketplace.Domain.Shipping.BuyerAggregate.Events;
using System;
using System.Collections.Generic;

namespace Marketplace.Domain.Shipping.BuyerAggregate
{
	public class Buyer : AggregateRoot
	{
		private readonly ICollection<string> orderIds = new List<string>();

		public IReadOnlyList<string> OrderIds => this.OrderIds;

		public void ConfirmShippementArrival(string orderId)
		{
			if (this.orderIds.Contains(orderId) == false)
				throw new InvalidOperationException();

			this.AddDomainEvent(new OrderArrivedEvent(orderId, this.Id));
		}
	}
}