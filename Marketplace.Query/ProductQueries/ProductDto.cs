using AutoMapper;
using AutoMapperRegistrar.Interfaces;
using Marketplace.Persistence.Sales;

namespace Marketplace.Query.ProductQueries
{
    public class ProductDto : IMappableFrom<ProductEntity>, ICustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string SellerId { get; set; }

        public string SellerName { get; set; }

        public string Status { get; set; }

        public PriceDto Price { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
			configuration.CreateMap<ProductEntity, ProductDto>()
				.ForPath(dest => dest.Price.Value, opt => opt.MapFrom(src => src.Price))
				.ForPath(dest => dest.Price.Currency, opt => opt.MapFrom(src => src.PriceCurrency));
		}
    }
}
