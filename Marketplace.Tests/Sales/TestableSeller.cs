using Marketplace.Domain.Sales.SellerAggregate;

namespace Marketplace.Tests.Sales
{
	public class TestableSeller : Seller
	{
		public TestableSeller()
		{

		}

		public TestableSeller(string id)
			: base()
		{
			this.Id = id;
		}
	}
}