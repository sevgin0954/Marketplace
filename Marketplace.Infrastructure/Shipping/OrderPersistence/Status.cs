namespace Marketplace.Infrastructure.Shipping.OrderPersistence
{
	public class Status
	{
		public Status(string name)
		{
			this.Name = name;
		}

		public string Id { get; set; }

		public string Name { get; set; }
	}
}
