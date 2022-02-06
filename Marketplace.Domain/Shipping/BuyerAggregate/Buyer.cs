using Marketplace.Domain.Common;
using Marketplace.Domain.Shipping.BuyerAggregate.Events;
using System;
using System.Collections.Generic;

namespace Marketplace.Domain.Shipping.BuyerAggregate
{
	public class Buyer : AggregateRoot
	{
		private readonly ICollection<string> OrderIds = new List<string>();

		public void ConfirmShippementArrival(string orderId)
		{
			if (this.OrderIds.Contains(orderId) == false)
				throw new InvalidOperationException();

			this.AddDomainEvent(new OrderArrivedEvent(orderId, this.Id));
		}
	}
}