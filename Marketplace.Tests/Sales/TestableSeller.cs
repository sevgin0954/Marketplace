using Marketplace.Domain.Sales;
using System.Collections.Generic;
using System.Reflection;

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

		public void AddProductToSold(string productId)
		{
			var dict = new Dictionary<string, Offer>();
			dict.Add(productId, null);

			var field = typeof(Seller)
				.GetField("soldProductIdsAndOffers", BindingFlags.NonPublic | BindingFlags.Instance);
			field.SetValue(this, dict);
		}
	}
}
