using AutoMapper;
using AutoMapperRegistrar.Interfaces;

namespace Marketplace.Persistence.Sales
{
	public class ProductEntity : IMappableBothDirections<Domain.Sales.ProductAggregate.Product>, ICustomMappings
	{
		public string Id { get; set; }

		public string SellerId { get; set; }
		public SellerEntity Seller { get; set; }

		public decimal Price { get; set; }
		public string PriceCurrency { get; set; }

		public string Status { get; set; }

		public void CreateMappings(IProfileExpression configuration)
		{
			configuration.CreateMap<Domain.Sales.ProductAggregate.Product, ProductEntity>()
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Value))
				.ForMember(dest => dest.PriceCurrency, opt => opt.MapFrom(src => src.Price.Currency));
		}
	}
}
