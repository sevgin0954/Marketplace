
using Marketplace.Domain.Sales.BuyerAggregate;
using Marketplace.Domain.Sales.ProductAggregate;
using Marketplace.Domain.Sales.SellerAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Persistence.Sales
{
	public class SalesDbContext : MarketplaceBaseDbContext
	{
		public SalesDbContext(string connectionString, bool isLoggingEnabled, IMediator mediator)
			: base(connectionString, isLoggingEnabled, mediator) { }

		public DbSet<Product> Products { get; set; }

		public DbSet<Seller> Sellers { get; set; }

		public DbSet<Buyer> Buyers { get; set; }
	}
}
