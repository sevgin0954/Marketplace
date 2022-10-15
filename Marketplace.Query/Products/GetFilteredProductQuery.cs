﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Marketplace.Persistence.Browsing;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Query.Products
{
	public class GetFilteredProductQuery : IRequest<IList<ProductDto>>
	{
		public GetFilteredProductQuery(ICollection<string> keywords)
		{
			this.Keywords = keywords;
		}

		public ICollection<string> Keywords { get; }

		internal class GetFilteredProductQueryHandler : IRequestHandler<GetFilteredProductQuery, IList<ProductDto>>
		{
			private readonly BrowsingDbContext dbContext;
			private readonly IMapper mapper;

			public GetFilteredProductQueryHandler(BrowsingDbContext dbContext, IMapper mapper)
			{
				this.dbContext = dbContext;
				this.mapper = mapper;
			}

			public async Task<IList<ProductDto>> Handle(GetFilteredProductQuery request, CancellationToken cancellationToken)
			{
				var keywordsLowerCase = request.Keywords.Select(k => k.ToLower());

				var products = await this.dbContext.Products
					.Where(p => 
						keywordsLowerCase.Any(k => p.Name.ToLower().Contains(k)) ||
						keywordsLowerCase.Any(k => p.Description.ToLower().Contains(k))
					).ProjectTo<ProductDto>(this.mapper.ConfigurationProvider)
					.ToListAsync();

				return products;
			}
		}
	}
}