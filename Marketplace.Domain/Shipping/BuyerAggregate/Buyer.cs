using Marketplace.Domain.Common;
using Marketplace.Domain.SharedKernel;
using Marketplace.Domain.Shipping.BuyerAggregate.Events;
using System;
using System.Collections.Generic;

namespace Marketplace.Domain.Shipping.BuyerAggregate
{
	public class Buyer : AggregateRoot<Id>
	{
		private readonly ICollection<string> orderIds = new List<string>();

		public Buyer(Id id)
			: base(id) { }

		public IReadOnlyList<string> OrderIds => this.OrderIds;

		public void ConfirmShippementArrival(string orderId)
		{
			if (this.orderIds.Contains(orderId) == false)
				throw new InvalidOperationException();

			this.AddDomainEvent(new OrderArrivedEvent(orderId, this.Id.Value));
		}
	}
}