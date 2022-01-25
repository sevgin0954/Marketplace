using Marketplace.Domain.Sales;

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
