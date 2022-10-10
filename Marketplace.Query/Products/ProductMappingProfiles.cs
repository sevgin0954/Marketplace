using AutoMapper;
using Marketplace.Persistence.Sales;

namespace Marketplace.Query.Products
{
	public class ProductMappingProfiles : Profile
	{
		public ProductMappingProfiles()
		{
			this.CreateMap<ProductEntity, ProductDto>()
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => new PriceDto()
				{
					Value = src.Price,
					Currency = src.PriceCurrency
				}));
		}
	}
}
