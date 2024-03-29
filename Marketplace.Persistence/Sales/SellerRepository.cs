﻿using AutoMapper;
using Marketplace.Domain.Sales.SellerAggregate;
using Marketplace.Domain.SharedKernel;

namespace Marketplace.Persistence.Sales
{
	public class SellerRepository : Repository<Seller, Id, SellerEntity>
	{
		public SellerRepository(SalesDbContext dbContext, IMapper mapper)
			: base(dbContext, mapper) { }
	}
}
