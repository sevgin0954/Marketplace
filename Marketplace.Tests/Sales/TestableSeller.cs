using Marketplace.Domain.Sales;
using Marketplace.Domain.Sales.SellerAggregate;
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

		public void AddProductToSelling(string productId)
		{
			var dict = new List<string>();
			dict.Add(productId);

			var field = typeof(Seller)
				.GetField("productIdsForSelling", BindingFlags.NonPublic | BindingFlags.Instance);
			field.SetValue(this, dict);
		}
	}
}
