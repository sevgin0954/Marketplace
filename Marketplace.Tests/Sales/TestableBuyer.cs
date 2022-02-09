using Marketplace.Domain.Sales.BuyerAggregate;

namespace Marketplace.Tests.Sales
{
	public class TestableBuyer : Buyer
	{
		public TestableBuyer(string id)
		{
			this.Id = id;
		}
	}
}
