using AutoMapper;
using Marketplace.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using SalesProduct = Marketplace.Domain.Sales.ProductAggregate.Product;

namespace Marketplace.Infrastructure.Sales.ProductPersistence
{
	public class ProductRepository : IRepository<SalesProduct>
	{
		private readonly ProductDbContext productDbContext;
		private readonly IMapper mapper;

		public ProductRepository(
			ProductDbContext productDbContext,
			IMapper mapper)
		{
			this.productDbContext = productDbContext;
			this.mapper = mapper;
		}

		public async Task<IList<SalesProduct>> GetAll()
		{
			var productEntities = await this.productDbContext.Products.ToListAsync();
			var productDomainModels = mapper.Map<IList<SalesProduct>>(productEntities);

			return productDomainModels;
		}

		public async Task<SalesProduct> GetByIdAsync(string id)
		{
			var productEntity = await this.productDbContext.Products.FindAsync(id);
			var productDomainModel = mapper.Map<SalesProduct>(productEntity);

			return productDomainModel;
		}

		public async Task<int> SaveChangesAsync()
		{
			return await this.productDbContext.SaveChangesAsync();
		}
	}
}