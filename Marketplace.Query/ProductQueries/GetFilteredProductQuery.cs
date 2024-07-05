using AutoMapper;
using AutoMapper.QueryableExtensions;
using Marketplace.Persistence.Browsing;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Query.ProductQueries
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

				// TODO: Add keywords to product aggregate
				var products = await this.FilterProductsByKeywords(this.dbContext.Products, keywordsLowerCase)
					.ProjectTo<ProductDto>(this.mapper.ConfigurationProvider)
					.ToListAsync();

				return products;
			}

			private IQueryable<ProductEntity> FilterProductsByKeywords(DbSet<ProductEntity> products, IEnumerable<string> keywords)
			{
				var filteredProducts = products.AsQueryable();

				foreach (var  keyword in keywords)
				{
					filteredProducts = filteredProducts.Where(p => p.Name.Contains(keyword));
				}

				return filteredProducts;
			}
		}
	}
}